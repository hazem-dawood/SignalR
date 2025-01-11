namespace SignalR.Application.Implementations.Chats;

public class GroupService(IApplicationDbContext applicationDbContext,
    ICurrentUserService currentUserService, IChatHubService chatHubService,
    ILogger<GroupService> logger, CancellationToken cancellationToken) : IGroupService
{
    public async Task<ResultDto<EditGroupDto>> Add(AddGroupDto model)
    {
        try
        {
            var entity = new Group
            {
                CreatedUserId = currentUserService.UserId!.Value,
                Name = model.Name,
                GroupUsers = [/*current user is also a member*/new GroupUser
                {
                    UserId = currentUserService.UserId!.Value,
                }, .. model.UserIds.Select(v => new GroupUser
                {
                    UserId = v
                })]
            };

            await applicationDbContext.Groups.AddAsync(entity);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            //notify users
            await chatHubService.NotifyUsersAddToGroup(new GetGroupDto
            {
                Name = entity.Name,
                Id = entity.Id,
                Members = entity.GroupUsers.Where(a => a.UserId != currentUserService.UserId)
                    .Select(v => new GetUserDto
                    {
                        Name = v.User.FullName,
                        Id = v.UserId,
                        ImageUrl = v.User.ImageUrl
                    }).ToList()
            });

            return new ResultDto<EditGroupDto>
            {
                IsSuccess = true,
                Messages = ["Success"],
                Data = new EditGroupDto
                {
                    Name = model.Name,
                    Id = entity.Id,
                    UserIds = model.UserIds
                }
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(Add));
        }

        return new ResultDto<EditGroupDto>
        {
            Messages = ["Error"]
        };
    }

    public async Task<ResultDto<List<GetGroupDto>>> GetCurrentUserGroups()
    {
        var data = await applicationDbContext
            .Groups
            .AsNoTracking()
            .Where(a => a.GroupUsers.Any(f => f.UserId == currentUserService.UserId))
            .Select(a => new GetGroupDto
            {
                Name = a.Name,
                Id = a.Id,
                Members = a.GroupUsers.Select(b => new GetUserDto
                {
                    Id = b.UserId,
                    Name = b.User.FullName,
                    ImageUrl = b.User.ImageUrl
                }).ToList(),
                LastMessage = a.Messages
                    .OrderByDescending(v => v.CreatedDate)
                    .Select(v => new LastMessageDto
                    {
                        CreatedDate = v.CreatedDate,
                        Message = v.Message
                    }).FirstOrDefault() ?? new LastMessageDto
                    {
                        CreatedDate = a.CreatedDate
                    },
                IsGroup = true
            }).ToListAsync(cancellationToken);

        return new ResultDto<List<GetGroupDto>>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public async Task<List<GetUserChatMessageDto>> GetGroupMessages(RequestGroupMessageDto model)
    {
        try
        {
            var data = await applicationDbContext
                .GroupMessages
                .AsNoTracking()
                .Where(a => a.GroupId == model.GroupId)
                .OrderByDescending(a => a.CreatedDate)
                .Select(a => new GetUserChatMessageDto
                {
                    CreatedDate = a.CreatedDate,
                    From = a.User.FullName,
                    Message = a.Message,
                    IsFromMe = a.UserId == currentUserService.UserId,
                })
                .Skip((model.PageNumber - 1) * model.Length)
                .Take(model.Length)
                .ToListAsync(cancellationToken);

            return data;
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(GetGroupMessages));
            //handle error
        }

        return [];
    }

    public async Task<ResultDto> SendMessage(SendGroupMessageDto model)
    {
        try
        {
            var group = await applicationDbContext
                .Groups
                .AsNoTracking()
                .Where(a => a.Id == model.GroupId)
                .Include(a => a.GroupUsers)
                .Select(a => new GetGroupDto
                {
                    Name = a.Name,
                    Id = a.Id,
                    Members = a.GroupUsers
                        .Where(v => v.UserId != currentUserService.UserId)
                        .Select(b => new GetUserDto
                        {
                            Id = b.UserId,
                            Name = b.User.FullName,
                            ImageUrl = b.User.ImageUrl
                        }).ToList(),
                    IsGroup = true
                }).FirstOrDefaultAsync(cancellationToken);

            if (group == null)
            {
                return new ResultDto { IsSuccess = false, Messages = ["Invalid Group Id"] };
            }

            var entity = await applicationDbContext.GroupMessages.AddAsync(new GroupMessage
            {
                Message = model.Message,
                GroupId = model.GroupId,
                UserId = currentUserService.UserId!.Value,
            });

            await applicationDbContext.SaveChangesAsync(cancellationToken);
            group.LastMessage = new LastMessageDto
            {
                CreatedDate = entity.Entity.CreatedDate,
                Message = model.Message,
            };

            await chatHubService.NotifyUsersAddToGroup(group);
            return new ResultDto { IsSuccess = true };
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(SendMessage));
            return new ResultDto { IsSuccess = false, Messages = ["Error"] };
        }
    }
}
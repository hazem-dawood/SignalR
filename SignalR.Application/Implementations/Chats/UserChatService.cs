namespace SignalR.Application.Implementations.Chats;

public class UserChatService(ILogger<UserChatService> logger,
    IApplicationDbContext applicationDbContext,
    ICurrentUserService currentUserService,
    IGroupService groupService,
    IChatHubService chatHubService,
    IApplicationUserService applicationUserService,
    CancellationToken cancellationToken) : IUserChatService
{
    public async Task<List<GetGroupDto>> GetUserChatsWithGroups()
    {
        try
        {
            var userChat = await applicationDbContext
                .UserChats
                .AsNoTracking()
                .Where(a =>
                    a.ToUserId == currentUserService.UserId || a.UserId == currentUserService.UserId)
                .Select(a => new GetGroupDto
                {
                    Id = a.Id,
                    Name = a.UserId == currentUserService.UserId ? a.ToUser.FullName : a.FromUser.FullName,
                    LastMessage = a.UserChatMessages.OrderByDescending(b => b.CreatedDate)
                        .Select(b => new LastMessageDto
                        {
                            CreatedDate = b.CreatedDate,
                            Message = b.Message
                        }).FirstOrDefault()!,
                    IsGroup = false,
                    IsOnline =
                        (a.UserId == currentUserService.UserId && a.ToUserId == currentUserService.UserId)
                        /*current user chat*/
                        || (
                            a.UserId == currentUserService.UserId
                            && a.ToUser.OnlineUser != null
                            && (DateTime.Now - a.ToUser.OnlineUser.LastModified).TotalSeconds < 60)
                        || (
                            a.ToUserId == currentUserService.UserId
                            && a.FromUser.OnlineUser != null
                            && (DateTime.Now - a.FromUser.OnlineUser.LastModified).TotalSeconds < 60)
                    ,
                    UserId = a.UserId == currentUserService.UserId ? a.ToUserId : a.UserId,
                    UserImage = a.UserId == currentUserService.UserId ? a.ToUser.ImageUrl : a.FromUser.ImageUrl,
                    LastSeen = a.UserId == currentUserService.UserId ?
                        (a.ToUser.OnlineUser != null ? a.ToUser.OnlineUser.LastModified : null) :
                        (a.FromUser.OnlineUser != null ? a.FromUser.OnlineUser.LastModified : null),
                })
                .ToListAsync(cancellationToken);

            var groupsResult = await groupService.GetCurrentUserGroups();

            if (groupsResult.IsSuccess)
            {
                return groupsResult.Data!.Concat(userChat)
                    .OrderByDescending(a => a.LastMessage?.CreatedDate).ToList();
            }

            return userChat;
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(GetUserChatsWithGroups));
            //handle error
        }
        return [];
    }

    public async Task<List<GetUserChatMessageDto>> GetUserChatMessages(RequestUserChatMessageDto model)
    {
        try
        {
            var data = await applicationDbContext
                .UserChatMessages
                .AsNoTracking()
                .Where(a => a.UserChatId == model.UserChatId)
                .OrderByDescending(a => a.CreatedDate)
                .Select(a => new GetUserChatMessageDto
                {
                    CreatedDate = a.CreatedDate,
                    From = a.UserChat.FromUser.FullName,
                    IsFromMe = a.CreatedUserId == currentUserService.UserId,
                    Message = a.Message,
                    To = a.UserChat.ToUser.FullName,
                    IsSeen = a.IsSeen
                })
                .Skip((model.PageNumber - 1) * model.Length).Take(model.Length)
                // why reverse ? to help js to append correctly.you can do it in js if you want.
                .Reverse()
                .ToListAsync(cancellationToken);

            return data;
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(GetUserChatMessages));
            //handle error
        }

        return [];
    }

    public async Task<ResultDto> MessagesSeen(int userChat)
    {
        var notSeenMessages = await applicationDbContext
            .UserChatMessages
            .AsNoTracking()
            .Where(a => !a.IsSeen && a.UserChatId == userChat && a.UserChat.UserId != currentUserService.UserId)
            .ToListAsync(cancellationToken);

        if (notSeenMessages.Count == 0)
            return new ResultDto { IsSuccess = true };

        foreach (var notSeenMessage in notSeenMessages)
        {
            notSeenMessage.IsSeen = true;
            applicationDbContext.Update(notSeenMessage);
        }

        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return new ResultDto { IsSuccess = true };
    }

    public async Task NotifyOtherUsersThatHeIsOnline(string? userId, bool isOnline)
    {
        if (!int.TryParse(userId, out var id))
            return;

        var users = await applicationUserService.GetUsers();
        var usersIds = users.Select(a => a.Id)
            //skip current user
            .Where(a => a != currentUserService.UserId).ToList();
        await chatHubService.NotifyOtherUsersThatHeIsOnlineOffline(usersIds, id);
    }

    public async Task<IndexViewModel> GetUsersWithChats()
    {
        var users = await applicationUserService.GetUsers();
        var vm = new IndexViewModel
        {
            Users = new SelectList(users, nameof(GetUserDto.UserName), nameof(GetUserDto.Name))
        };

        if (currentUserService.UserId == null)
            return vm;

        vm.Chats = await GetUserChatsWithGroups();
        vm.NewUsers = users.Where(a => vm.Chats.All(b => b.UserId != a.Id)).ToList();

        return vm;
    }
}
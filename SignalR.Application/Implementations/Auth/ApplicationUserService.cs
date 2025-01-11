namespace SignalR.Application.Implementations.Auth;

public class ApplicationUserService(ILogger<ApplicationUserService> logger,
    IApplicationDbContext applicationDbContext,
    Lazy<ICurrentUserService> currentUserService,
    CancellationToken cancellationToken) : IApplicationUserService
{
    public async Task<List<GetUserDto>> GetUsers()
    {
        try
        {
            var currentUser = currentUserService.Value;
            return await applicationDbContext
                .Users
                .AsNoTracking()
                .Select(a => new GetUserDto
                {
                    Id = a.Id,
                    Name = a.FullName,
                    ImageUrl = a.ImageUrl,
                    UserName = a.UserName,
                    LastSeen = a.OnlineUser != null ? a.OnlineUser.LastModified : a.CreatedDate,
                    IsOnline = (a.OnlineUser != null
                                && (DateTime.Now - a.OnlineUser.LastModified)
                                .TotalSeconds < 60) || a.Id == currentUser.UserId
                })
                .ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(GetUsers));
            //handle error
        }
        return [];
    }

    public async Task<List<IdNameDto>> GetUserGroups(int userId)
    {
        try
        {
            return await applicationDbContext
                .GroupUsers
                .AsNoTracking()
                .Include(a => a.Group)
                .Where(a => a.UserId == userId)
                .Select(a => new IdNameDto
                {
                    Id = a.GroupId,
                    Name = a.Group.Name
                }).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(GetUserGroups));
            //handle error
        }

        return [];
    }

    public async Task<ResultDto<GetUserDto>> SignIn(LogInDto model)
    {
        var userExists = await applicationDbContext
                .Users
                .AsNoTracking()
                .Where(a => a.UserName == model.UserName && a.Password == model.Password)
                .Select(a => new GetUserDto
                {
                    Id = a.Id,
                    Name = a.FullName,
                    ImageUrl = a.ImageUrl,
                    UserName = a.UserName
                })
                .FirstOrDefaultAsync(cancellationToken);

        return new ResultDto<GetUserDto>
        {
            IsSuccess = userExists != null,
            Data = userExists
        };
    }
}
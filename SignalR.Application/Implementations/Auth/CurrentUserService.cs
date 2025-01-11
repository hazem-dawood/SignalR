namespace SignalR.Application.Implementations.Auth;

public class CurrentUserService(IApplicationDbContext applicationDbContext,
    IHttpContextAccessor httpContextAccessor,
    IApplicationUserService applicationUserService,
    IChatHubService chatHubService) : ICurrentUserService
{
    private ApplicationUser? _currentUser;
    private int? _userId;

    public int? UserId
    {
        get
        {
            var context = httpContextAccessor.HttpContext;

            if (_userId != null || context == null)
                return _userId; //CustomUserIdProvider.IdClaimName
            //of course you can change claim type
            var claim = context.User.Claims.FirstOrDefault(a => a.Type == CustomUserIdProvider.IdClaimName);
            if (claim == null)
                return _userId;

            if (int.TryParse(claim.Value, out var id))
                _userId = id;

            return _userId;
        }
    }

    public ApplicationUser? CurrentUser
    {
        get
        {
            return _currentUser ??= applicationDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == UserId);
        }
    }

    public List<Claim> GetLogInClaims(GetUserDto model)
    {
        return
        [
            new Claim("FullName", model.Name),
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim(CustomUserIdProvider.IdClaimName, model.Id.ToString())
        ];
    }

    public async Task SignOut()
    {
        if (UserId != null)
        {
            await chatHubService
                .NotifyOtherUsersThatHeIsOnlineOffline(
                    (await applicationUserService.GetUsers()).Where(a => a.Id != UserId)
                    .Select(a => a.Id)
                    .ToList(), UserId ?? 0, false);
        }
    }
}
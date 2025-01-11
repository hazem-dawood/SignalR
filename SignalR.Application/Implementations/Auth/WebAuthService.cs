namespace SignalR.Application.Implementations.Auth;

public class WebAuthService(ICurrentUserService currentUserService,
    IHttpContextAccessor httpContextAccessor,
    IApplicationUserService applicationUserService) : IWebAuthService
{
    public async Task<ResultDto<GetUserDto>> SignIn(LogInDto model)
    {
        var userResult = await applicationUserService.SignIn(model);

        if (!userResult.IsSuccess)
            return userResult;

        var claims = currentUserService.GetLogInClaims(userResult.Data!);

        // Create claims identity
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Create authentication properties (optional)
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Keep the user logged in across browser sessions
            ExpiresUtc = DateTimeOffset.UtcNow.AddYears(20) // Set expiration time
        };

        // Sign in the user
        await httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return userResult;
    }

    public async Task SignOut()
    {
        await currentUserService.SignOut();
        await httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
namespace SignalR.Api.Controllers;

public class UserController(IApplicationUserService applicationUserService,
    IJwtService jwtService, ICurrentUserService currentUserService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserGroups(int userId) => Ok(await applicationUserService.GetUserGroups(userId));

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetUsers() => Ok(await applicationUserService.GetUsers());

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> SignIn(LogInDto model)
    {
        var res = await applicationUserService.SignIn(model);
        if (res.IsSuccess)
            res.Data!.Token = jwtService.GetJwtToken(res.Data!);
        return Ok(res);
    }

    [HttpPost]
    public new async Task<IActionResult> SignOut()
    {
        await currentUserService.SignOut();
        return Ok();
    }
}
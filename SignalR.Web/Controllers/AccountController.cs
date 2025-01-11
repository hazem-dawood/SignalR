namespace SignalR.Web.Controllers;

public class AccountController(IWebAuthService webAuthService) : Controller
{
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<JsonResult> SignIn(LogInDto model) => Json(await webAuthService.SignIn(model));

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await webAuthService.SignOut();
        return Redirect("~/");
    }
}
namespace SignalR.Web.Controllers;

public class HomeController(IUserChatService chatService,
    IUserChatMessageService userChatMessageService) : Controller
{
    public async Task<ViewResult> Index() 
        => View(await chatService.GetUsersWithChats());

    #region  Ajax

    [HttpGet, Authorize]
    public async Task<IActionResult> GetChats()
        => PartialView("_Chats", (await Index()).Model);

    [HttpGet, Authorize]
    public async Task<IActionResult> GetChatMessages([FromQuery] RequestUserChatMessageDto model)
        => Json(await chatService.GetUserChatMessages(model));

    [HttpPost, Authorize]
    public async Task<JsonResult> SendMessage(AddMessageDto model)
        => Json(await userChatMessageService.Add(model));

    #endregion
}
namespace SignalR.Api.Controllers;

public class UserChatController(IUserChatService userChatService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserChatsWithGroups() => Ok(await userChatService.GetUserChatsWithGroups());

    [HttpGet]
    public async Task<IActionResult> GetUserChatMessages([FromQuery] RequestUserChatMessageDto model) => Ok(await userChatService.GetUserChatMessages(model));

    [HttpPost]
    public async Task<IActionResult> MessagesSeen(int userChatId) => Ok(await userChatService.MessagesSeen(userChatId));
}
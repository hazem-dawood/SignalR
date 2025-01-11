namespace SignalR.Api.Controllers;

public class UserChatMessageController(IUserChatMessageService userChatMessageService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]AddMessageDto model) => Ok(await userChatMessageService.Add(model));
}
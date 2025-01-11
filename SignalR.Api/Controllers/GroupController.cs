namespace SignalR.Api.Controllers;

public class GroupController(IGroupService groupService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add(AddGroupDto model) => Ok(await groupService.Add(model));

    [HttpGet]
    public async Task<IActionResult> GetCurrentUserGroups() => Ok(await groupService.GetCurrentUserGroups());

    [HttpGet]
    public async Task<IActionResult> GetGroupMessages([FromQuery] RequestGroupMessageDto model)
        => Ok(await groupService.GetGroupMessages(model));

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendGroupMessageDto model) =>
        Ok(await groupService.SendMessage(model));
}
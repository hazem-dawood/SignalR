namespace SignalR.Api.Controllers;

[ApiController, Route("api/[controller]/[action]"), Authorize] //[Area("Test")]
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase;
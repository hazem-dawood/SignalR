namespace SignalR.Api.Controllers;

/// <summary>
/// Signalr <see cref="ControllerBase"/> to share attributes
/// <see cref="ApiControllerAttribute"/> ,
/// <see cref="RouteAttribute"/> ,
/// <see cref="AuthorizeAttribute"/>
/// </summary>
[ApiController, Route("api/[controller]/[action]"), Authorize] //[Area("Test")]
public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase;
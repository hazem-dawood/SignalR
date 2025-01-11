namespace SignalR.Application.Hubs;

/// <summary>
/// This hub is responsible for sending and receiving messages,manage groups,....
/// </summary>
[SignalRHub(Path, description: "This hub is responsible for sending and receiving messages,manage groups,....")]
public class ChatHub(IOnlineUserService onlineUserService, IUserChatService userChatService,
    ILogger<ChatHub> logger) : Hub<IChatHubClient>
{
    public const string Path = "/chatHub";

    //Context.UserIdentifier
    // this come from ClaimTypes.NameIdentifier or custom from CustomUserIdProvider

    /// <summary>
    /// tells us who is online ?
    /// </summary>
    /// <returns></returns>
    [SignalRMethod(nameof(OnConnectedAsync), description: " you can't invoke it. it called when user connected to hub")]
    public override async Task OnConnectedAsync()
    {
        await onlineUserService.AddEdit(Context.UserIdentifier, true);
        await userChatService.NotifyOtherUsersThatHeIsOnline(Context.UserIdentifier, true);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// offline
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    [SignalRMethod(nameof(OnConnectedAsync), description: " you can't invoke it. it called when user disconnected from hub")]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception != null)
            logger.LogError(exception, nameof(OnDisconnectedAsync));

        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// logged-in user must call this to update the user status
    /// </summary>
    /// <returns></returns>
    [SignalRMethod(nameof(UpdateUserStatus), description: "Update User Status")]
    public async Task UpdateUserStatus()
    {
        await onlineUserService.AddEdit(Context.UserIdentifier, true);
    }
}
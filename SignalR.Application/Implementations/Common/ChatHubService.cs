namespace SignalR.Application.Implementations.Common;

/// <summary>
/// please use this service inside other services
/// this service is responsible for operation over the <see cref="ChatHub"/> inside services.
/// you can access the hub though it at anywhere in the application
/// <code></code>
/// why using this service and not using <see cref="IHubContext{THub}"/> direct?
/// <code></code>
/// imagine in the future you want also to implement firebase also, so you will edit your logic here
/// no need to edit at anywhere else.
/// </summary>
internal class ChatHubService(IHubContext<ChatHub, IChatHubClient> hubContext) : IChatHubService
{
    public async Task NotifyUsersAddToGroup(GetGroupDto model)
    {
        await hubContext
             .Clients
             .Users(model.Members.Select(a => a.Id.ToString()))
             .AddedToGroup(model);
    }

    public async Task NotifyUserOfMessage(NotifyUserMessageDto model)
    {
        await hubContext
            .Clients
            .User(model.ToUserId.ToString())
            .MessageAdded(model);
    }

    public async Task NotifyOtherUsersThatHeIsOnlineOffline(List<int> usersIds, int userId
        , bool isOnline = true)
    {
        if (isOnline)
            await hubContext.Clients
                .Users(usersIds.Select(a => a.ToString()))
                .NewOnlineUser(userId);
        else
            await hubContext.Clients
            .Users(usersIds.Select(a => a.ToString()))
            .OfflineUser(userId);
    }
}
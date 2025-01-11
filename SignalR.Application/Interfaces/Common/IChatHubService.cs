namespace SignalR.Application.Interfaces.Common;

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
public interface IChatHubService
{
    /// <summary>
    /// current user add some users to a group
    /// we need to notify them with the group name and members
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task NotifyUsersAddToGroup(GetGroupDto model);

    /// <summary>
    /// after user send a message to another
    /// we need to notify the second member with the message
    /// </summary>
    /// <param name="notifyUserMessageDto"></param>
    /// <returns></returns>
    public Task NotifyUserOfMessage(NotifyUserMessageDto notifyUserMessageDto);

    /// <summary>
    /// notify other user that current user is online
    /// </summary>
    /// <param name="usersIds"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task NotifyOtherUsersThatHeIsOnlineOffline(List<int> usersIds, int userId, bool isOnline = true);
}
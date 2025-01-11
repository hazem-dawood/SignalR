namespace SignalR.Application.Interfaces.Common;

/// <summary>
/// No Implementation for this interface
/// this only to make hub (strongly type)
/// <code></code>
/// من الاخر مش محتاج اكتب اسم الميثود بايدي في ال js.
/// </summary>
public interface IChatHubClient
{
    /// <summary>
    /// method name in js
    /// ex: .on('AddedToGroup',()=>{ /* do whatever */ })
    /// </summary>
    /// <returns></returns>
    public Task AddedToGroup(GetGroupDto model);

    /// <summary>
    /// chat between two persons
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task MessageAdded(NotifyUserMessageDto model);

    /// <summary>
    /// notify other user that current user is online
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task NewOnlineUser(int userId);

    /// <summary>
    /// after user sign out
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task OfflineUser(int userId);
}
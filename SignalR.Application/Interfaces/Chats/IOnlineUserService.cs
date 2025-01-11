namespace SignalR.Application.Interfaces.Chats;

/// <summary>
/// user online or offline
/// </summary>
public interface IOnlineUserService
{
    /// <summary>
    /// add new user status or modify user status online or offline
    /// </summary>
    /// <param name="userId">why string ? signalr has it as string</param>
    /// <param name="isOnline"></param>
    /// <returns></returns>
    public Task AddEdit(string? userId, bool isOnline);
}
namespace SignalR.Application.Interfaces.Chats;

public interface IUserChatService
{
    /// <summary>
    /// get user chats or groups that he is a member of it.
    /// </summary>
    /// <returns></returns>
    public Task<List<GetGroupDto>> GetUserChatsWithGroups();

    /// <summary>
    /// pagination chat messages by id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<List<GetUserChatMessageDto>> GetUserChatMessages(RequestUserChatMessageDto model);

    /// <summary>
    /// set message as seen
    /// </summary>
    /// <param name="userChatId"></param>
    /// <returns></returns>
    public Task<ResultDto> MessagesSeen(int userChatId);

    /// <summary>
    /// after a user become online we need to notify other users
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="isOnline"></param>
    /// <returns></returns>
    public Task NotifyOtherUsersThatHeIsOnline(string? userId, bool isOnline);

    /// <summary>
    /// get for index
    /// </summary>
    /// <returns></returns>
    public Task<IndexViewModel> GetUsersWithChats();
}
namespace SignalR.Application.Interfaces.Chats;

/// <summary>
/// manage logic over <see cref="Group"/>
/// </summary>
public interface IGroupService
{
    /// <summary>
    /// add new group
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<ResultDto<EditGroupDto>> Add(AddGroupDto model);

    /// <summary>
    /// get the groups that the current user is a member of it.
    /// </summary>
    /// <returns></returns>
    public Task<ResultDto<List<GetGroupDto>>> GetCurrentUserGroups();

    /// <summary>
    /// pagination group messages by group id
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<List<GetUserChatMessageDto>> GetGroupMessages(RequestGroupMessageDto model);

    /// <summary>
    /// send a message to a group
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<ResultDto> SendMessage(SendGroupMessageDto model);
}
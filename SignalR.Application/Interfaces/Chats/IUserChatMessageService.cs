namespace SignalR.Application.Interfaces.Chats;

/// <summary>
/// manage chats messages
/// </summary>
public interface IUserChatMessageService
{
    /// <summary>
    /// send new message
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<ResultDto<AddMessageDto>> Add(AddMessageDto model);
}
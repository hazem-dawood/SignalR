namespace SignalR.Application.Implementations.Chats;

public class UserChatMessageService(IApplicationDbContext applicationDbContext,
    ILogger<UserChatMessageService> logger,
    ICurrentUserService currentUserService,
    IChatHubService chatHubService,
    CancellationToken cancellationToken) : IUserChatMessageService
{
    public async Task<ResultDto<AddMessageDto>> Add(AddMessageDto model)
    {
        try
        {
            ResultDto<UserChatMessage> result;

            var userChat = await applicationDbContext
                .UserChats
                .Where(a =>
                    // user send the first message before
                    a.ToUserId == model.ToUserId && a.UserId == currentUserService.UserId
                    //user receives the first message before
                    || a.ToUserId == currentUserService.UserId && a.UserId == model.ToUserId
                ).FirstOrDefaultAsync(cancellationToken);

            if (model.UserChatId == 0 && userChat == null)
                //first message
                result = await AddNewUserChat(model);
            else
                //add new message
                result = await AddNewMessage(model, userChat!);
            

            //skip if user are talking to himself
            if (result.IsSuccess && currentUserService.UserId != model.ToUserId)
            {
                //notify user of the new message
                await chatHubService.NotifyUserOfMessage(new NotifyUserMessageDto
                {
                    FromFullName = currentUserService.CurrentUser?.FullName ?? "",
                    Message = model.Message,
                    MessageId = result.Data!.Id,
                    UserChatId = result.Data!.UserChatId,
                    ToUserId = model.ToUserId,
                    CreatedDate = result.Data!.CreatedDate
                });
            }
            return new ResultDto<AddMessageDto>
            {
                IsSuccess = result.IsSuccess,
                Messages = result.Messages,
                Data = new AddMessageDto
                {
                    ToUserId = model.ToUserId,
                    UserChatId = result.Data!.UserChatId
                }
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, nameof(Add));
            //handle error
        }
        return new ResultDto<AddMessageDto> { Messages = ["Error"] };
    }

    #region Helpers

    /// <summary>
    /// add new message to already existing chat
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userChat"></param>
    /// <returns></returns>
    private async Task<ResultDto<UserChatMessage>> AddNewMessage(AddMessageDto model, UserChat userChat)
    {
        var userChatMessage = new UserChatMessage
        {
            UserChatId = userChat.Id,
            Message = model.Message,
            CreatedUserId = currentUserService.UserId!.Value
        };

        await applicationDbContext.UserChatMessages.AddAsync(userChatMessage, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return new ResultDto<UserChatMessage>
        {
            IsSuccess = true,
            Messages = ["Success"],
            Data = userChatMessage
        };
    }

    /// <summary>
    /// if first time to chat
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private async Task<ResultDto<UserChatMessage>> AddNewUserChat(AddMessageDto model)
    {
        var userChatMessage = new UserChatMessage()
        {
            Message = model.Message,
            CreatedUserId = currentUserService.UserId!.Value
        };
        var entity = new UserChat
        {
            ToUserId = model.ToUserId,
            UserId = currentUserService.UserId!.Value,
            UserChatMessages = [userChatMessage]
        };
        await applicationDbContext
            .UserChats
            .AddAsync(entity, cancellationToken);

        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return new ResultDto<UserChatMessage>
        {
            IsSuccess = true,
            Messages = ["Success"],
            Data = userChatMessage
        };
    }

    #endregion
}
namespace SignalR.Application.Models;


public class RequestUserChatMessageDto : RequestPaginationDto
{
    public int UserChatId { get; set; }

}

public class GetUserChatMessageDto
{
    public string From { get; set; } = string.Empty;

    public bool IsFromMe { get; set; }

    public string To { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public bool IsSeen { get; set; }
}

public class AddMessageDto
{
    public int UserChatId { get; set; }

    public int ToUserId { get; set; }

    public string Message { get; set; } = string.Empty;
}

public class LastMessageDto
{
    public string Message { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}

public class NotifyUserMessageDto
{
    public int UserChatId { get; set; }

    public int MessageId { get; set; }

    public string Message { get; set; } = string.Empty;

    public string FromFullName { get; set; } = string.Empty;

    public int ToUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}
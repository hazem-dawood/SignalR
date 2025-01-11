namespace SignalR.Application.Models;

public class AddGroupDto
{
    /// <summary>
    /// group name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// group members ids
    /// </summary>
    public List<int> UserIds { get; set; } = [];
}

/// <summary>
/// send a message to a group
/// we need group id and the message
/// </summary>
public class SendGroupMessageDto
{
    /// <summary>
    /// group
    /// </summary>
    public int GroupId { get; set; }

    /// <summary>
    /// message to sent
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

public class EditGroupDto : AddGroupDto
{
    /// <summary>
    /// group id
    /// </summary>
    public int Id { get; set; }
}

public class GetGroupDto : IdNameDto, IUserOnlineDto
{
    public List<GetUserDto> Members { get; set; } = [];

    public LastMessageDto LastMessage { get; set; } = new();

    public bool IsGroup { get; set; }
    public int UserId { get; set; }
    public string UserImage { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public DateTime? LastSeen { get; set; }
}

public class RequestGroupMessageDto : RequestPaginationDto
{
    public int GroupId { get; set; }
}
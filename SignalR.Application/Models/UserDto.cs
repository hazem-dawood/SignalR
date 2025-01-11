namespace SignalR.Application.Models;

public class GetUserDto : IdNameDto, IUserOnlineDto
{
    public string ImageUrl { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public bool IsOnline { get; set; }

    public DateTime? LastSeen { get; set; }
    public string Token { get; set; } = string.Empty;
}
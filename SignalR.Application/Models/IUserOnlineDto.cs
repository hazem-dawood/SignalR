namespace SignalR.Application.Models;

public interface IUserOnlineDto
{
    public DateTime? LastSeen { get; set; }
}
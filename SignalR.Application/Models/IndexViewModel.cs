namespace SignalR.Application.Models;

public class IndexViewModel
{
    public SelectList Users { get; set; }

    public List<GetGroupDto> Chats { get; set; } = [];

    /// <summary>
    /// users witch chats
    /// </summary>
    public List<GetUserDto> NewUsers { get; set; } = [];
}
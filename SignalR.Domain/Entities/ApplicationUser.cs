namespace SignalR.Domain.Entities;

/// <summary>
/// users
/// </summary>
public class ApplicationUser : BaseEntity
{
    /// <summary>
    /// full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// username to log in
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// password to log in not encrypted
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public string ImageUrl { get; set; }

    #region Relations

    /// <summary>
    /// One-to-one relationship
    /// </summary>
    public OnlineUser? OnlineUser { get; set; }

    #endregion

    #region Collections

    /// <summary>
    /// The groups that he created them
    /// </summary>
    public virtual ICollection<Group> Groups { get; set; }

    /// <summary>
    /// navigation that he is a member in a group
    /// </summary>
    public virtual ICollection<GroupUser> GroupUsers { get; set; }

    /// <summary>
    /// get all messages that he sent in groups
    /// </summary>
    public virtual ICollection<GroupMessage> GroupMessages { get; set; }

    /// <summary>
    /// the chats he started
    /// </summary>
    public virtual ICollection<UserChat> FromMessages { get; set; }

    /// <summary>
    /// the chats he is the second member
    /// </summary>
    public virtual ICollection<UserChat> ToMessages { get; set; }

    /// <summary>
    /// the messages he sent in a chat between two members
    /// </summary>
    public virtual ICollection<UserChatMessage> UserChatMessages { get; set; }

    #endregion

}
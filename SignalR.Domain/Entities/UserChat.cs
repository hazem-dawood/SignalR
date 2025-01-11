namespace SignalR.Domain.Entities;

/// <summary>
/// chat between two members only
/// </summary>
public class UserChat : BaseEntity
{
    #region Relations

    /// <summary>
    /// member one and he starts messaging (who created the chat)
    /// </summary>
    [ForeignKey(nameof(FromUser))]
    public int UserId { get; set; }

    /// <summary>
    /// member one and he starts messaging (who created the chat)
    /// </summary>
    public virtual ApplicationUser FromUser { get; set; }

    /// <summary>
    /// the second member
    /// </summary>
    [ForeignKey(nameof(ToUser))]
    public int ToUserId { get; set; }

    /// <summary>
    /// the second member
    /// </summary>
    public virtual ApplicationUser ToUser { get; set; }

    #endregion

    #region Collections

    /// <summary>
    /// Navigation to messages
    /// </summary>
    public virtual ICollection<UserChatMessage> UserChatMessages { get; set; }

    #endregion
}

/// <summary>
/// messages between two members
/// </summary>
public class UserChatMessage : BaseEntity
{
    /// <summary>
    /// is message seen or not
    /// </summary>
    public bool IsSeen { get; set; }

    /// <summary>
    /// Message Text
    /// </summary>
    public string Message { get; set; } = string.Empty;

    #region Relations

    /// <summary>
    /// Chat <see cref="UserChat"/>
    /// </summary>
    [ForeignKey(nameof(UserChat))]
    public int UserChatId { get; set; }

    public virtual UserChat UserChat { get; set; }

    /// <summary>
    /// who sent the message
    /// </summary>
    [ForeignKey(nameof(CreatedUser))]
    public int CreatedUserId { get; set; }

    /// <summary>
    /// who sent the message
    /// </summary>
    public virtual ApplicationUser CreatedUser { get; set; }

    #endregion
}
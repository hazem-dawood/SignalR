namespace SignalR.Domain.Entities;

/// <summary>
/// group contains name and who created it (the admin)
/// </summary>
public class Group : BaseEntity
{
    /// <summary>
    /// name of the group
    /// </summary>
    public string Name { get; set; } = string.Empty;

    #region Relations

    /// <summary>
    /// Who created the group
    /// </summary>
    [ForeignKey(nameof(CreatedUser))]
    public int CreatedUserId { get; set; }

    /// <summary>
    /// Who created the group
    /// </summary>
    public virtual ApplicationUser CreatedUser { get; set; }

    #endregion

    #region Collections

    /// <summary>
    /// navigation to members
    /// </summary>
    public virtual ICollection<GroupUser> GroupUsers { get; set; }

    /// <summary>
    /// navigation to users
    /// </summary>
    public virtual ICollection<GroupMessage> Messages { get; set; }

    #endregion
}

/// <summary>
/// group users (also deleted)
/// members
/// </summary>
public class GroupUser : BaseEntity
{

    #region Relations

    /// <summary>
    /// group member
    /// </summary>
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// group member
    /// </summary>
    public virtual ApplicationUser User { get; set; }

    /// <summary>
    /// <see cref="Group"/>
    /// </summary>
    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }

    /// <summary>
    /// <see cref="Group"/>
    /// </summary>
    public virtual Group Group { get; set; }

    #endregion
}

/// <summary>
/// message in a group between users
/// </summary>
public class GroupMessage : BaseEntity
{
    /// <summary>
    /// message text
    /// </summary>
    public string Message { get; set; } = string.Empty;

    #region Relations

    /// <summary>
    /// relation to <see cref="Group"/>
    /// </summary>
    [ForeignKey(nameof(Group))]
    public int GroupId { get; set; }

    public virtual Group Group { get; set; }

    /// <summary>
    /// who sent the message
    /// </summary>
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    /// <summary>
    /// who sent the message
    /// </summary>
    public virtual ApplicationUser User { get; set; }

    #endregion
}
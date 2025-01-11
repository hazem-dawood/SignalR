namespace SignalR.Domain.Entities;

/// <summary>
/// who's online ?one-to-one relationship with <see cref="ApplicationUser"/>
/// </summary>
public class OnlineUser : BaseEntity
{
    /// <summary>
    /// can be last online date
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.Now;

    #region Relations

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    #endregion
}
namespace SignalR.Domain.Common;

/// <summary>
/// all entities must inherit from it
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary Key
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// created date default <see cref="DateTime.Now"/>
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// is this row deleted or not default is false
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
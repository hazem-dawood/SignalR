namespace SignalR.Application.Interfaces.Common;

/// <summary>
/// please use Repository instead of db context
/// </summary>
public interface IApplicationDbContext
{
    public EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());

    public DbSet<ApplicationUser> Users { get; }
    public DbSet<Group> Groups { get; }
    public DbSet<GroupUser> GroupUsers { get; }
    public DbSet<GroupMessage> GroupMessages { get; }
    public DbSet<UserChat> UserChats { get; }
    public DbSet<UserChatMessage> UserChatMessages { get; }
    public DbSet<OnlineUser> OnlineUsers { get; }
}
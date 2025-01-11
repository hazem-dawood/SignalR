namespace SignalR.Persistence.DbContexts;

public partial class ApplicationDbContext
{
    #region Auth

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    #endregion

    #region Chats

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<GroupUser> GroupUsers => Set<GroupUser>();

    public DbSet<GroupMessage> GroupMessages => Set<GroupMessage>();

    public DbSet<UserChat> UserChats => Set<UserChat>();

    public DbSet<UserChatMessage> UserChatMessages => Set<UserChatMessage>();

    public DbSet<OnlineUser> OnlineUsers => Set<OnlineUser>();

    #endregion
}
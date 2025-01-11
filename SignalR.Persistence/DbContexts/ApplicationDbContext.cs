namespace SignalR.Persistence.DbContexts;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //must be first line so we can override it .
        //It is very helpful if you are using IdentityDbContext
        base.OnModelCreating(modelBuilder);

        modelBuilder
            //apply configurations
            .ApplyConfigurationsFromAssembly(typeof(UserChatConfiguration).Assembly)
            .SeedData();
    }
}
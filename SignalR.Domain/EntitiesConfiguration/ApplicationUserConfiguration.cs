namespace SignalR.Domain.EntitiesConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(nameof(ApplicationUser) + "s", SchemaConstants.Auth);

        #region one to many

        builder.HasMany(a => a.Groups)
            .WithOne(a => a.CreatedUser)
            .HasForeignKey(a => a.CreatedUserId);

        builder.HasMany(a => a.GroupUsers)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        builder.HasMany(a => a.GroupMessages)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        builder.HasMany(a => a.FromMessages)
            .WithOne(a => a.FromUser)
            .HasForeignKey(a => a.UserId);

        builder.HasMany(a => a.ToMessages)
            .WithOne(a => a.ToUser)
            .HasForeignKey(a => a.ToUserId);

        builder.HasMany(a => a.UserChatMessages)
            .WithOne(a => a.CreatedUser)
            .HasForeignKey(a => a.CreatedUserId);

        #endregion

        //one to one 
        builder.HasOne(a => a.OnlineUser)
            .WithOne(a => a.User)
            .HasForeignKey<OnlineUser>(a => a.UserId);
    }
}
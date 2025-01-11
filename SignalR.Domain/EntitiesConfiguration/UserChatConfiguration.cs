namespace SignalR.Domain.EntitiesConfiguration;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.ToTable(nameof(UserChat) + "s", SchemaConstants.Chats);
    }
}

public class UserChatMessageConfiguration : IEntityTypeConfiguration<UserChatMessage>
{
    public void Configure(EntityTypeBuilder<UserChatMessage> builder)
    {
        builder.ToTable(nameof(UserChatMessage) + "s", SchemaConstants.Chats);
    }
}
namespace SignalR.Domain.EntitiesConfiguration;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable(nameof(Group) + "s", SchemaConstants.Chats);
    }
}

public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.ToTable(nameof(GroupUser) + "s", SchemaConstants.Chats);
    }
}

public class GroupMessageConfiguration : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
    {
        builder.ToTable(nameof(GroupMessage) + "s", SchemaConstants.Chats);
    }
}
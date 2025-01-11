namespace SignalR.Persistence.Seeding;

public static class SeedEntities
{
    /// <summary>
    /// seeding data
    /// it contains seeding application users
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <returns></returns>
    public static ModelBuilder SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ApplicationUser>()
            .HasData(new ApplicationUser
            {
                Id = 1,
                FullName = "Hazem Dawood",
                UserName = "hazem",
                Password = "123456",
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                ImageUrl = "https://bootdey.com/img/Content/avatar/avatar2.png"
            },
            new ApplicationUser
            {
                Id = 2,
                FullName = "Ahmed Emad",
                UserName = "ahmed",
                Password = "123456",
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                ImageUrl = "https://bootdey.com/img/Content/avatar/avatar1.png"
            },
            new ApplicationUser
            {
                Id = 3,
                FullName = "Kareem Belal",
                UserName = "kareem",
                Password = "123456",
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                ImageUrl = "https://bootdey.com/img/Content/avatar/avatar4.png"
            },
            new ApplicationUser
            {
                Id = 4,
                FullName = "Mahmoud Bahrawy",
                UserName = "bahrawy",
                Password = "123456",
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                ImageUrl = "https://bootdey.com/img/Content/avatar/avatar7.png"
            });

        modelBuilder.Entity<OnlineUser>()
            .HasData(new OnlineUser
            {
                Id = 1,
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                LastModified = new DateTime(2024, 12, 23, 0, 0, 0),
                UserId = 1
            },
            new OnlineUser
            {
                Id = 2,
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                LastModified = new DateTime(2024, 12, 23, 0, 0, 0),
                UserId = 2
            },
            new OnlineUser
            {
                Id = 3,
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                LastModified = new DateTime(2024, 12, 23, 0, 0, 0),
                UserId = 3
            },
            new OnlineUser
            {
                Id = 4,
                CreatedDate = new DateTime(2024, 12, 23, 0, 0, 0),
                LastModified = new DateTime(2024, 12, 23, 0, 0, 0),
                UserId = 4
            });
        return modelBuilder;
    }
}
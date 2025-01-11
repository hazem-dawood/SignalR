namespace SignalR.Persistence;

public static class Startup
{
    /// <summary>
    /// Register all services that needed in this Layer (Class Library)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder BuildPersistence(this WebApplicationBuilder builder)
    {
        var connectionsStringsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            "connectionStrings.json");

        builder.Configuration.AddJsonFile(connectionsStringsFile);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlite("Data Source=app.db")); // SQLite database file
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return builder.BuildApplication();
    }

    /// <summary>
    /// add needed Middlewares in this Layer
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UsePersistence(this WebApplication app)
    {
        // Ensure database is created
        try
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate(); // This will apply any pending migrations
        }
        catch (Exception e)
        {
            _ = e;
        }
        return app.UseApplication();
    }
}


namespace SignalR.Domain;

public static class Startup
{
    /// <summary>
    /// Register all services that needed in this Layer (Class Library)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder BuildDomain(this WebApplicationBuilder builder)
    {
        return builder;
    }

    /// <summary>
    /// add needed Middlewares in this Layer
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseDomain(this WebApplication app)
    {
        return app;
    }
}

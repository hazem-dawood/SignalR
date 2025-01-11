namespace SignalR.Application;

public static class Startup
{
    /// <summary>
    /// Register all services that needed in this Layer (Class Library)
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder BuildApplication(this WebApplicationBuilder builder)
    {
        builder.BuildDomain();
        //add signal r
        builder.Services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            }).AddHubOptions<ChatHub>(options =>
            {
                options.EnableDetailedErrors = true; // Enable detailed errors
            })

        #region For Load Balancer (Multi servers) enable next line for redis (or you can use Azure,Sticky session ,.....)

        //.AddStackExchangeRedis("localhost:6379")

        #endregion
        .Services

            // tell signal r how to generate user id.
            //by default signalr uses claim ClaimTypes.NameIdentifier
            //so we needed to overwrite it.

            //signal r
            .AddSingleton<IUserIdProvider, CustomUserIdProvider>()
            //signal r
            .AddScoped<IChatHubService, ChatHubService>()

            .AddScoped<IUserChatService, UserChatService>()
            .AddScoped<IUserChatMessageService, UserChatMessageService>()
            .AddScoped<IApplicationUserService, ApplicationUserService>()
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddScoped<IOnlineUserService, OnlineUserService>()
            .AddScoped<IGroupService, GroupService>()

            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IWebAuthService, WebAuthService>()

            .AddScoped(typeof(Lazy<>), typeof(LazyServiceProvider<>))

            .AddHttpContextAccessor()

            // register Cancellation Token globally
            .AddScoped(typeof(CancellationToken), serviceProvider =>
            {
                var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
            });

        return builder;
    }


    /// <summary>
    /// add needed Middlewares in this Layer
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseApplication(this WebApplication app)
    {
        app.MapHub<ChatHub>(ChatHub.Path, options =>
        {
            //
        });
        return app.UseDomain();
    }
}

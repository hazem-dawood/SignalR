namespace SignalR.Api.Helpers;

public static class AppAuthenticationExtension
{
    public static WebApplicationBuilder AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var config = new JwtConfigurationDto();
                builder.Configuration.GetSection("Jwt").Bind(config);
                var tokenValidation = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key)),
                    ValidAudience = config.ValidAudience,
                    ValidIssuer = config.ValidIssuer,
                    ValidateAudience = config.ValidateAudience,
                    ValidateIssuer = config.ValidateIssuer,
                    ValidateIssuerSigningKey = config.ValidateIssuerSigningKey,
                    ValidateLifetime = config.ValidateLifetime
                };
                options.TokenValidationParameters = tokenValidation;
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context => context.Return401Response(),
                    OnMessageReceived = context =>
                    {
                        //authenticate hub so in CustomUserIdProvider can be IsAuthenticated = true
                        if (!context.Request.Path.Value?.Contains(ChatHub.Path,
                                StringComparison.CurrentCultureIgnoreCase) == true)
                            return Task.CompletedTask;

                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                            context.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            })
            .Services
            .AddAuthorization();

        return builder;
    }
}
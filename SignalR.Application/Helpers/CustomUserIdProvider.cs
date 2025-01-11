namespace SignalR.Application.Helpers;

/// <summary>
/// tell signal r how to generate user id !
/// by default signal r users <see cref="ClaimTypes.NameIdentifier"/> as the user id if it's exists
/// </summary>
public class CustomUserIdProvider : IUserIdProvider
{
    /// <summary>
    /// value is id of user
    /// </summary>
    public static string IdClaimName => ClaimTypes.NameIdentifier;

    public string? GetUserId(HubConnectionContext connection)
    {
        var httpContext = connection.GetHttpContext();
        if (httpContext is { User.Identity.IsAuthenticated: true })
        {
            //user authenticated
            //how user authenticated in web and api ?
            //in web, we are using cookie authentication so, it worked fine.
            //in api we need to pass the token while connection to the hub
            // so how we tell app to receives the token?
            // in API project Program find AddJwtBearer => Events => OnMessageReceived
            var claimType = httpContext.User.Claims.FirstOrDefault(a => a.Type == IdClaimName);
            if (claimType != null)
                return claimType.Value;
        }
        else if (httpContext != null)
        {
            // Handle unauthenticated users for SignalR
            // There are several options to handle this scenario, but here's a practical approach:

            // Goal: Register a guest user and send notifications without requiring login.

            // Solution: 
            // 1. On the frontend, generate a unique identifier (e.g., a GUID) for the guest user.
            // 2. Store this identifier in the browser's local storage.
            // 3. Send this identifier as a query string parameter when initializing the SignalR connection.
            //.withUrl(window.signalrConfiguration.route + '?UserKey=test')
            var id = httpContext.Request.Query["UserKey"];
            //so now we return it and every time the connection failed he will connect at the same user
            // so in hub we can send by SendToUser({id})
            return id;
        }
        return Guid.NewGuid().ToString();
    }
}
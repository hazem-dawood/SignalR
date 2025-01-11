namespace SignalR.Application.Helpers;

public static class HttpResponseExtensions
{
    /// <summary>
    /// customize the response of 401 <see cref="StatusCodes.Status401Unauthorized"/>
    /// to return body <see cref="ResultDto{T}"/>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Task Return401Response(this JwtBearerChallengeContext context)
    {
        // Custom response
        context.HandleResponse();
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        var result = new ResultDto
        {
            IsSuccess = false,
            Messages = ["Un authorized Please Log In First"]
        };
        return context.Response.WriteAsJsonAsync(result);
    }
}
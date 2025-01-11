namespace SignalR.Application.Helpers;

/// <summary>
/// extension methods
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// is user online depends on <see cref="IUserOnlineDto.LastSeen"/> must has value and value must be less than 60 seconds
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="user"></param>
    /// <returns></returns>
    public static bool IsUserOnline<T>(this T user) where T : IUserOnlineDto
    {
        return user.LastSeen != null && (DateTime.Now - user.LastSeen!.Value).TotalSeconds < 60;
    }

    /// <summary>
    /// get full name from claims 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string GetFullName(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(a => a.Type == "FullName")?.Value ?? "";
    }

    /// <summary>
    /// get 10 chars 
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    public static string SubStringMessage(this LastMessageDto? messageDto)
    {
        if (messageDto == null || string.IsNullOrWhiteSpace(messageDto.Message))
            return string.Empty;

        return messageDto.Message.Length < 10 ? messageDto.Message : messageDto.Message[..10];
    }
}
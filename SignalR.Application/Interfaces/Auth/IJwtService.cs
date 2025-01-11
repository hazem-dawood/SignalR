namespace SignalR.Application.Interfaces.Auth;

public interface IJwtService
{
    /// <summary>
    /// Generate a token for the user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public string GetJwtToken(GetUserDto user);
}
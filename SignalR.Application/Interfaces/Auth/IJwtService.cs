namespace SignalR.Application.Interfaces.Auth;

public interface IJwtService
{
    public string GetJwtToken(GetUserDto user);
}
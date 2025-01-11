namespace SignalR.Application.Interfaces.Auth;

public interface IWebAuthService
{
    public Task<ResultDto<GetUserDto>> SignIn(LogInDto model);

    public Task SignOut();
}
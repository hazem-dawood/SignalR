namespace SignalR.Application.Interfaces.Auth;

public interface IApplicationUserService
{
    /// <summary>
    /// get all users inside db
    /// </summary>
    /// <returns></returns>
    public Task<List<GetUserDto>> GetUsers();

    /// <summary>
    /// get groups of a user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<List<IdNameDto>> GetUserGroups(int userId);

    /// <summary>
    /// log in
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<ResultDto<GetUserDto>> SignIn(LogInDto model);
}
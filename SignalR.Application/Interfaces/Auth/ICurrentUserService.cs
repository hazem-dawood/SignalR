namespace SignalR.Application.Interfaces.Auth;

public interface ICurrentUserService
{
    /// <summary>
    /// return current user id
    /// </summary>
    public int? UserId { get; }

    /// <summary>
    /// return current user info
    /// </summary>
    public ApplicationUser? CurrentUser { get; }

    /// <summary>
    /// if you have multiple application so, you must share your claims between them
    /// </summary>
    /// <param name="userResultData"></param>
    /// <returns></returns>
    public List<Claim> GetLogInClaims(GetUserDto userResultData);

    /// <summary>
    /// sign out
    /// set user to offline
    /// </summary>
    /// <returns></returns>
    public Task SignOut();
}
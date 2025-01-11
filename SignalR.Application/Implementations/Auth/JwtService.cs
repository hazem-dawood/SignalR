namespace SignalR.Application.Implementations.Auth;

public class JwtService(ICurrentUserService currentUserService,
    IConfiguration configuration) : IJwtService
{
    public string GetJwtToken(GetUserDto user)
    {
        var claims = currentUserService.GetLogInClaims(user);
        var config = new JwtConfigurationDto();
        configuration.GetSection("Jwt").Bind(config);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config.ValidIssuer,
            audience: config.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(config.ValidMinutes),
            signingCredentials: credentials);

        //expiration = token.ValidTo

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
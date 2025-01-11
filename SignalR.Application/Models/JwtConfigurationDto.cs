namespace SignalR.Application.Models;

public class JwtConfigurationDto
{
    public bool ValidateIssuer { get; set; }

    public bool ValidateAudience { get; set; }

    public bool ValidateLifetime { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public string ValidIssuer { get; set; } = string.Empty;

    public string ValidAudience { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public int ValidMinutes { get; set; }
}

namespace EcoTracker.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshExpiry { get; set; }

}
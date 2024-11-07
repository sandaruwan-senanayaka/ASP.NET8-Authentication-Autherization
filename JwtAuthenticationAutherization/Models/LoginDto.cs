namespace JwtAuthenticationAutherization.Model;

public class LoginDto
{
    public required String Email { get; set; }
    public required String Password { get; set; }
}

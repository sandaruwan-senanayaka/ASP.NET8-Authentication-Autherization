namespace JwtAuthenticationAutherization.Model;

//public class LoginDto
//{
//    public required string Email { get; set; }
//    public required string Password { get; set; }
//}

public record LoginDto(string Email, string Password);


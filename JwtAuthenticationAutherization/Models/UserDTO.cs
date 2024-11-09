namespace JwtAuthenticationAutherization.Models;

//public class UserDTO
//{
//    public required string Name { get; set; }
//    public required string Email { get; set; }
//    public required string Password { get; set; }
//}

public record UserDTO(string Name, string Email, string Password);

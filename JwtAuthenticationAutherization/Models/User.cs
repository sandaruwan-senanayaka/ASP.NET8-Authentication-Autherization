namespace JwtAuthenticationAutherization.Model;

public record User
//public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int IsActive { get; set; } = 1;
    public DateTime CreateOn { get; set; } = DateTime.Now;
}


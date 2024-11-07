namespace JwtAuthenticationAutherization.Model;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IsActive { get; set; } = 1;
    public DateTime CreateOn { get; set; } = DateTime.Now;
}

using JwtAuthenticationAutherization.Model;
using JwtAuthenticationAutherization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationAutherization.Controllers;
public class UserController : Controller
{

    private readonly MyDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public UserController(MyDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Registration")]
    public IActionResult Registration(UserDTO userDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var objUser = _dbContext.User
            .FirstOrDefault(u => u.Email.Equals(userDTO.Email));

        if (objUser is not null)
        {
            return BadRequest("User already exists with same email address");
        }

        _dbContext.User.Add(new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            Password = userDTO.Password,
        });
        _dbContext.SaveChanges();
        return Ok("User registration success");

    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login(LoginDto loginDto)
    {
        var user = _dbContext.User
            .FirstOrDefault(x => x.Email.Equals(loginDto.Email)
            && x.Password.Equals(loginDto.Password));

        if (user is null)
        {
            return Unauthorized();
        }
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signIn
            );
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            Token = tokenValue,
            User = user
        });

    }

    [HttpGet]
    [Route("GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_dbContext.User.ToList());
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("GetUser")]
    public IActionResult GetUser(int id)
    {
        var user = _dbContext.User.
            FirstOrDefault(x => x.UserId == id);

        if (user is null)
        {
            return NoContent();
        }

        return Ok(user);

    }
}

using Microsoft.AspNetCore.Mvc;
using SessionAuthentication.Database;
using SessionAuthentication.Database.Datasets;
using SessionAuthentication.Utils;

namespace SessionAuthentication.Controllers;


[ApiController]
[Route("[controller]")]
public class Authentication(DataContext context) : ControllerBase
{
    public class RegistrationRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
    
    [HttpPost("/login")]
    public ActionResult Login()
    {
        return Ok("test");
    }  
    
    [HttpPost("/register")]
    public ActionResult Register([FromBody] RegistrationRequest request)
    {
        // Check if user already exists
        // If user exists, return 400 Bad Request
        var isUserExists = context.Users.Any(user => user.Username == request.Username || user.Email == request.Email);
        if (isUserExists)
        {
            return BadRequest(new { message = "User already exists" });
        }
        
        // Hash password for security
        // Im using SHA256 here, but you can use any other hashing algorithm
        var hashedPassword = StringUtils.Sha256Encrypt(request.Password);
        
        // Create new user object
        var user = new User
        {
            Username = request.Username,
            Password = hashedPassword,
            Email = request.Email
        };
        
        // Save user to database
        context.Users.Add(user);
        
        // Commit changes
        context.SaveChanges();
        
        // Return success message
        return Ok(new {message = "User created successfully" });
    }  
}
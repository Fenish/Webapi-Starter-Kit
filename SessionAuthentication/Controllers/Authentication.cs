using Microsoft.AspNetCore.Mvc;
using SessionAuthentication.Database;
using SessionAuthentication.Database.Datasets;
using SessionAuthentication.Utils;

namespace SessionAuthentication.Controllers;


public class RegistrationRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}

public class LoginRequest
{
    public required string Identifier { get; set; }
    public required string Password { get; set; }
}

[ApiController]
[Route("[controller]")]
public class Authentication(DatabaseContext context) : ControllerBase
{
    [HttpPost("/login")]
    public ActionResult Login([FromBody] LoginRequest request)
    {
        var identifier = request.Identifier;
        var hashedPassword = StringUtils.Sha256Encrypt(request.Password);
        
        // Check if user exists
        var user = context.Users.FirstOrDefault(user => user.Username == identifier || user.Email == identifier);
        
        // If user does not exist, return 400 Bad Request
        if (user == null)
        {
            return BadRequest(new { message = "User does not exist" });
        }
        
        // Check if password is correct
        if (user.Password != hashedPassword)
        {
            return BadRequest(new { message = "Password is incorrect" });
        }
        
        // Create new session
        var tokenData = Session.CreateSession(user.Username);
        
        // Set cookies for the client
        Response.Cookies.Append("accessToken", tokenData.AccessToken, new CookieOptions
        {
            HttpOnly = true, // Makes the cookie inaccessible from client-side scripts
            Secure = true, // Sends the cookie over HTTPS only
            SameSite = SameSiteMode.Strict, // Helps prevent cross-site request forgery (CSRF) attacks
        });

        Response.Cookies.Append("refreshToken", tokenData.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
        
        // Return session token
        return Ok(new
        {
            tokenData.AccessToken,
            tokenData.RefreshToken
        });
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
using Microsoft.AspNetCore.Mvc;
using SessionAuthentication.Database;
using SessionAuthentication.Utils;

namespace SessionAuthentication.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController(DatabaseContext context) : ControllerBase
{
    private static Dictionary<string, string> ProcessCookies(string cookies)
    {
        var cookiePairs = cookies.Split("; ");
        return cookiePairs.Select(cookiePair => cookiePair.Split("=")).ToDictionary(cookie => cookie[0], cookie => cookie[1]);
    }
    
    [HttpGet("/users/@me")]
    public ActionResult GetAuthenticatedUser()
    {   
        // Check if access token is provided
        if (!Request.Cookies.TryGetValue("accessToken", out var accessToken))
        {
            return BadRequest(new { message = "Access token is not provided" });
        }
        
        // Check if session exists
        if (!Session.SessionExists(accessToken))
        {
            return BadRequest(new { message = "Session does not exist" });
        }
        
        // Get session data
        var session = Session.GetSession(accessToken);
        var username = session.Username;
        
        // Get user
        var user = context.Users.FirstOrDefault(user => user.Username == username);
        
        // If user does not exist, return 400 Bad Request
        if (user == null)
        {
            return BadRequest(new { message = "User does not exist" });
        }
        
        // Return user
        return Ok(new
        {
            user.Username,
            user.Email
        });
    }  
}
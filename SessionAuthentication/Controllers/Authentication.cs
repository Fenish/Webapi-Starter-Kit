using Microsoft.AspNetCore.Mvc;

namespace SessionAuthentication.Controllers;

[Route("[controller]")]
public class Authentication: ControllerBase
{
    [HttpPost("/login")]
    public ActionResult Login()
    {
        return Ok("test");
    }  
    
    [HttpPost("/register")]
    public ActionResult Register()
    {
        return Ok("Registered");
    }  
}
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Controllers;

[ApiController]
[Route("[controller]")]
public class Books: ControllerBase
{
    [HttpGet("all")]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}
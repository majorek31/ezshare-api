using Microsoft.AspNetCore.Mvc;

namespace EzShare.WebAPI.Controllers;

public class HelloController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok("Hello, World!");
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using rest_api_core.Models;

namespace rest_api_core.Controllers;

[Route("/api/[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   [HttpGet]
    public IActionResult Index()
    {
        return Ok(new
        {
            message = "Hello World"
        });
    }

    [HttpGet("message")]
    public IActionResult Message()
    {
        return Ok(new
        {
            message = "success",
            code = 200
        });
    }
    
}

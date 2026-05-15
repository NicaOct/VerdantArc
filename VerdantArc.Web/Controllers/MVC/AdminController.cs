using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VerdantArc.Web.Controllers.MVC;

[Authorize(Roles = "Admin")]
[Route("Admin")]
public class AdminController : Controller
{
    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet("Trails")]
    public IActionResult Trails()
    {
        return View();
    }

    [HttpGet("Species")]
    public IActionResult Species()
    {
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace VerdantArc.Web.Controllers.MVC;

public class WeatherController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

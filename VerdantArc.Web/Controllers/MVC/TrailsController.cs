using Microsoft.AspNetCore.Mvc;
using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Controllers.MVC;

public class TrailsController : Controller
{
    private readonly ITrailService _trailService;

    public TrailsController(ITrailService trailService)
    {
        _trailService = trailService;
    }

    public async Task<IActionResult> Index()
    {
        var trails = await _trailService.GetAllAsync(1, 100);
        return View(trails);
    }

    public async Task<IActionResult> Details(int id)
    {
        var trail = await _trailService.GetByIdAsync(id);
        if (trail == null) return NotFound();
        return View(trail);
    }
}

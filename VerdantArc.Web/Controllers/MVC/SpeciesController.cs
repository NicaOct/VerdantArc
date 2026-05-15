using Microsoft.AspNetCore.Mvc;
using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Controllers.MVC;

public class SpeciesController : Controller
{
    private readonly ISpeciesService _speciesService;

    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    public async Task<IActionResult> Index(string? category)
    {
        var species = await _speciesService.GetAllAsync(category);
        return View(species);
    }

    public async Task<IActionResult> Details(int id)
    {
        var species = await _speciesService.GetByIdAsync(id);
        if (species == null) return NotFound();
        return View(species);
    }
}

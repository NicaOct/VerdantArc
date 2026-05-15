using Microsoft.AspNetCore.Mvc;
using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Controllers.Api;

[ApiController]
[Route("api/v1/species")]
public class SpeciesApiController : ControllerBase
{
    private readonly ISpeciesService _speciesService;

    public SpeciesApiController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpeciesResponseDto>>> GetSpecies([FromQuery] string? category = null)
    {
        var species = await _speciesService.GetAllAsync(category);
        return Ok(species);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SpeciesResponseDto>> GetSpecies(int id)
    {
        var species = await _speciesService.GetByIdAsync(id);
        if (species == null) return NotFound();
        return Ok(species);
    }
}

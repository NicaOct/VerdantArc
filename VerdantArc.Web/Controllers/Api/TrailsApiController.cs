using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;

namespace VerdantArc.Web.Controllers.Api;

[ApiController]
[Route("api/v1/trails")]
public class TrailsApiController : ControllerBase
{
    private readonly ITrailService _trailService;

    public TrailsApiController(ITrailService trailService)
    {
        _trailService = trailService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrailResponseDto>>> GetTrails([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var trails = await _trailService.GetAllAsync(page, pageSize);
        return Ok(trails);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrailResponseDto>> GetTrail(int id)
    {
        var trail = await _trailService.GetByIdAsync(id);
        if (trail == null) return NotFound();
        return Ok(trail);
    }

    [HttpPost("{id}/report")]
    [Authorize]
    public IActionResult AddReport(int id)
    {
        // Placeholder for adding a report
        return Created($"/api/v1/trails/{id}/report", new { message = "Report added successfully" });
    }

    [HttpGet("chatbot-filter")]
    public async Task<ActionResult<IEnumerable<TrailResponseDto>>> ChatbotFilter(
        [FromQuery] string? difficulty,
        [FromQuery] string? distance,
        [FromQuery] string? duration,
        [FromQuery] string? elevation,
        [FromQuery] string? gpx)
    {
        var trails = await _trailService.FilterTrailsAsync(difficulty, distance, duration, elevation, gpx);
        return Ok(trails);
    }
}

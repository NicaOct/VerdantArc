using VerdantArc.Domain.Enums;

namespace VerdantArc.Application.DTOs;

public class TrailResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DifficultyLevel { get; set; } = string.Empty;
    public decimal DistanceKm { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int ElevationGainM { get; set; }
    public string? GpxFileUrl { get; set; }
    public string? ImageUrl { get; set; }
}

public class TrailCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public decimal DistanceKm { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int ElevationGainM { get; set; }
    public string? ImageUrl { get; set; }
}

public class TrailUpdateDto : TrailCreateDto
{
    public int Id { get; set; }
}

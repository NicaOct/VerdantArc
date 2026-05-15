using VerdantArc.Domain.Enums;

namespace VerdantArc.Domain.Entities;

public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public decimal DistanceKm { get; set; }
    public int EstimatedDurationMinutes { get; set; }
    public int ElevationGainM { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? GpxFileUrl { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<TrailSighting> TrailSightings { get; set; } = new List<TrailSighting>();
    public ICollection<VisitorReport> VisitorReports { get; set; } = new List<VisitorReport>();
}

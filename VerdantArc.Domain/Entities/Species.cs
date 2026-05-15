using VerdantArc.Domain.Enums;

namespace VerdantArc.Domain.Entities;

public class Species
{
    public int Id { get; set; }
    public string ScientificName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public Category Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public ConservationStatus ConservationStatus { get; set; }
    public string? ImageUrl { get; set; }
    public string? InatNaturalistId { get; set; }
    public bool IsEndemic { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TrailSighting> TrailSightings { get; set; } = new List<TrailSighting>();
}

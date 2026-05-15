namespace VerdantArc.Domain.Entities;

public class TrailSighting
{
    public int Id { get; set; }
    public int TrailId { get; set; }
    public Trail Trail { get; set; } = null!;

    public int SpeciesId { get; set; }
    public Species Species { get; set; } = null!;

    public DateTime ObservedAt { get; set; }
    public string? Notes { get; set; }

    public string ReportedByUserId { get; set; } = string.Empty;
    public ApplicationUser ReportedByUser { get; set; } = null!;
}

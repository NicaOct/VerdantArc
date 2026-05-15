namespace VerdantArc.Domain.Entities;

public class VisitorReport
{
    public int Id { get; set; }
    
    public int TrailId { get; set; }
    public Trail Trail { get; set; } = null!;

    public DateTime VisitDate { get; set; }
    public int ConditionRating { get; set; } // 1-5
    public string Comment { get; set; } = string.Empty;

    public string SubmittedByUserId { get; set; } = string.Empty;
    public ApplicationUser SubmittedByUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

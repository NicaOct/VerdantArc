using Microsoft.AspNetCore.Identity;

namespace VerdantArc.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public ICollection<TrailSighting> TrailSightings { get; set; } = new List<TrailSighting>();
    public ICollection<VisitorReport> VisitorReports { get; set; } = new List<VisitorReport>();
}

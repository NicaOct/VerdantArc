namespace VerdantArc.Web.Models;

public class UserAdminViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }
    public List<string> Roles { get; set; } = new();
}

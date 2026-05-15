using VerdantArc.Domain.Enums;

namespace VerdantArc.Application.DTOs;

public class SpeciesResponseDto
{
    public int Id { get; set; }
    public string ScientificName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ConservationStatus { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? InatNaturalistId { get; set; }
    public bool IsEndemic { get; set; }
}

public class SpeciesCreateDto
{
    public string ScientificName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public Category Category { get; set; }
    public string Description { get; set; } = string.Empty;
    public ConservationStatus ConservationStatus { get; set; }
    public string? ImageUrl { get; set; }
    public string? InatNaturalistId { get; set; }
    public bool IsEndemic { get; set; }
}

public class SpeciesUpdateDto : SpeciesCreateDto
{
    public int Id { get; set; }
}

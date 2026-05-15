using VerdantArc.Application.DTOs;

namespace VerdantArc.Application.Interfaces;

public interface ISpeciesService
{
    Task<IEnumerable<SpeciesResponseDto>> GetAllAsync(string? category = null);
    Task<SpeciesResponseDto?> GetByIdAsync(int id);
    Task<SpeciesResponseDto> CreateAsync(SpeciesCreateDto dto);
    Task UpdateAsync(int id, SpeciesUpdateDto dto);
    Task DeleteAsync(int id);
}

using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;
using VerdantArc.Domain.Entities;
using VerdantArc.Domain.Enums;

namespace VerdantArc.Application.Services;

public class SpeciesService : ISpeciesService
{
    private readonly IRepository<Species> _repository;

    public SpeciesService(IRepository<Species> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SpeciesResponseDto>> GetAllAsync(string? category = null)
    {
        var all = await _repository.GetAllAsync();
        
        if (!string.IsNullOrEmpty(category) && Enum.TryParse<Category>(category, true, out var catEnum))
        {
            all = all.Where(s => s.Category == catEnum);
        }

        return all.Select(s => new SpeciesResponseDto
        {
            Id = s.Id,
            ScientificName = s.ScientificName,
            CommonName = s.CommonName,
            Category = s.Category.ToString(),
            Description = s.Description,
            ConservationStatus = s.ConservationStatus.ToString(),
            ImageUrl = s.ImageUrl,
            InatNaturalistId = s.InatNaturalistId,
            IsEndemic = s.IsEndemic
        });
    }

    public async Task<SpeciesResponseDto?> GetByIdAsync(int id)
    {
        var s = await _repository.GetByIdAsync(id);
        if (s == null) return null;

        return new SpeciesResponseDto
        {
            Id = s.Id,
            ScientificName = s.ScientificName,
            CommonName = s.CommonName,
            Category = s.Category.ToString(),
            Description = s.Description,
            ConservationStatus = s.ConservationStatus.ToString(),
            ImageUrl = s.ImageUrl,
            InatNaturalistId = s.InatNaturalistId,
            IsEndemic = s.IsEndemic
        };
    }

    public async Task<SpeciesResponseDto> CreateAsync(SpeciesCreateDto dto)
    {
        var species = new Species
        {
            ScientificName = dto.ScientificName,
            CommonName = dto.CommonName,
            Category = dto.Category,
            Description = dto.Description,
            ConservationStatus = dto.ConservationStatus,
            ImageUrl = dto.ImageUrl,
            InatNaturalistId = dto.InatNaturalistId,
            IsEndemic = dto.IsEndemic
        };

        await _repository.AddAsync(species);

        return await GetByIdAsync(species.Id) ?? throw new Exception("Failed to retrieve created species");
    }

    public async Task UpdateAsync(int id, SpeciesUpdateDto dto)
    {
        var species = await _repository.GetByIdAsync(id);
        if (species != null)
        {
            species.ScientificName = dto.ScientificName;
            species.CommonName = dto.CommonName;
            species.Category = dto.Category;
            species.Description = dto.Description;
            species.ConservationStatus = dto.ConservationStatus;
            species.ImageUrl = dto.ImageUrl;
            species.InatNaturalistId = dto.InatNaturalistId;
            species.IsEndemic = dto.IsEndemic;

            await _repository.UpdateAsync(species);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

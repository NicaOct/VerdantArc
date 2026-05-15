using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;
using VerdantArc.Domain.Entities;

namespace VerdantArc.Application.Services;

public class TrailService : ITrailService
{
    private readonly IRepository<Trail> _repository;

    public TrailService(IRepository<Trail> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrailResponseDto>> GetAllAsync(int page = 1, int pageSize = 10)
    {
        var trails = await _repository.GetAllAsync();
        return trails.Skip((page - 1) * pageSize).Take(pageSize).Select(t => new TrailResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            DifficultyLevel = t.DifficultyLevel.ToString(),
            DistanceKm = t.DistanceKm,
            EstimatedDurationMinutes = t.EstimatedDurationMinutes,
            ElevationGainM = t.ElevationGainM,
            GpxFileUrl = t.GpxFileUrl,
            ImageUrl = t.ImageUrl
        });
    }

    public async Task<TrailResponseDto?> GetByIdAsync(int id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;
        
        return new TrailResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            DifficultyLevel = t.DifficultyLevel.ToString(),
            DistanceKm = t.DistanceKm,
            EstimatedDurationMinutes = t.EstimatedDurationMinutes,
            ElevationGainM = t.ElevationGainM,
            GpxFileUrl = t.GpxFileUrl,
            ImageUrl = t.ImageUrl
        };
    }

    public async Task<TrailResponseDto> CreateAsync(TrailCreateDto dto)
    {
        var trail = new Trail
        {
            Name = dto.Name,
            Description = dto.Description,
            DifficultyLevel = dto.DifficultyLevel,
            DistanceKm = dto.DistanceKm,
            EstimatedDurationMinutes = dto.EstimatedDurationMinutes,
            ElevationGainM = dto.ElevationGainM,
            ImageUrl = dto.ImageUrl
        };

        await _repository.AddAsync(trail);

        return await GetByIdAsync(trail.Id) ?? throw new Exception("Failed to retrieve created trail");
    }

    public async Task UpdateAsync(int id, TrailUpdateDto dto)
    {
        var trail = await _repository.GetByIdAsync(id);
        if (trail != null)
        {
            trail.Name = dto.Name;
            trail.Description = dto.Description;
            trail.DifficultyLevel = dto.DifficultyLevel;
            trail.DistanceKm = dto.DistanceKm;
            trail.EstimatedDurationMinutes = dto.EstimatedDurationMinutes;
            trail.ElevationGainM = dto.ElevationGainM;
            trail.ImageUrl = dto.ImageUrl;
            trail.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(trail);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

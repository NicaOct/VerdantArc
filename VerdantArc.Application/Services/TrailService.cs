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

    public async Task<IEnumerable<TrailResponseDto>> FilterTrailsAsync(string? difficulty, string? distance, string? duration, string? elevation, string? gpx)
    {
        var trails = await _repository.GetAllAsync();

        // 1. Difficulty filter
        if (!string.IsNullOrEmpty(difficulty) && !difficulty.Equals("any", StringComparison.OrdinalIgnoreCase))
        {
            if (Enum.TryParse<VerdantArc.Domain.Enums.DifficultyLevel>(difficulty, true, out var diffEnum))
            {
                trails = trails.Where(t => t.DifficultyLevel == diffEnum);
            }
        }

        // 2. Distance filter (Short < 5km / Medium 5-10km / Long > 10km / Any)
        if (!string.IsNullOrEmpty(distance) && !distance.Equals("any", StringComparison.OrdinalIgnoreCase))
        {
            if (distance.Equals("short", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.DistanceKm < 5m);
            else if (distance.Equals("medium", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.DistanceKm >= 5m && t.DistanceKm <= 10m);
            else if (distance.Equals("long", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.DistanceKm > 10m);
        }

        // 3. Duration filter (Quick < 120m / Moderate 120-300m / Long > 300m / Any)
        if (!string.IsNullOrEmpty(duration) && !duration.Equals("any", StringComparison.OrdinalIgnoreCase))
        {
            if (duration.Equals("quick", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.EstimatedDurationMinutes < 120);
            else if (duration.Equals("moderate", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.EstimatedDurationMinutes >= 120 && t.EstimatedDurationMinutes <= 300);
            else if (duration.Equals("long", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.EstimatedDurationMinutes > 300);
        }

        // 4. Elevation filter (Flat < 150m / Moderate 150-600m / Steep > 600m / Any)
        if (!string.IsNullOrEmpty(elevation) && !elevation.Equals("any", StringComparison.OrdinalIgnoreCase))
        {
            if (elevation.Equals("flat", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.ElevationGainM < 150);
            else if (elevation.Equals("moderate", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.ElevationGainM >= 150 && t.ElevationGainM <= 600);
            else if (elevation.Equals("steep", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => t.ElevationGainM > 600);
        }

        // 5. GPX map filter (Yes / No / Any)
        if (!string.IsNullOrEmpty(gpx) && !gpx.Equals("any", StringComparison.OrdinalIgnoreCase))
        {
            if (gpx.Equals("yes", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => !string.IsNullOrEmpty(t.GpxFileUrl));
            else if (gpx.Equals("no", StringComparison.OrdinalIgnoreCase))
                trails = trails.Where(t => string.IsNullOrEmpty(t.GpxFileUrl));
        }

        return trails.Select(t => new TrailResponseDto
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
}

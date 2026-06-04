using VerdantArc.Application.DTOs;

namespace VerdantArc.Application.Interfaces;

public interface ITrailService
{
    Task<IEnumerable<TrailResponseDto>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<TrailResponseDto?> GetByIdAsync(int id);
    Task<TrailResponseDto> CreateAsync(TrailCreateDto dto);
    Task UpdateAsync(int id, TrailUpdateDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<TrailResponseDto>> FilterTrailsAsync(string? difficulty, string? distance, string? duration, string? elevation, string? gpx);
}

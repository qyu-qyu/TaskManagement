using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface IPriorityService
    {
        Task<List<PriorityResponseDto>> GetAllAsync();

        Task<PriorityResponseDto?> GetByIdAsync(int id);

        Task<PriorityResponseDto> CreateAsync(PriorityDto dto);

        Task<bool> UpdateAsync(int id, PriorityDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
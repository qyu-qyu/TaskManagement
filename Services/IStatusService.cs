using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface IStatusService
    {
        Task<List<StatusResponseDto>> GetAllAsync();

        Task<StatusResponseDto?> GetByIdAsync(int id);

        Task<StatusResponseDto> CreateAsync(StatusDto dto);

        Task<bool> UpdateAsync(int id, StatusDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
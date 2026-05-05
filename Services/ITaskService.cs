using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface ITaskService
    {
        Task<List<TaskResponseDto>> GetAllTasksAsync(string userId);
        Task<TaskResponseDto?> GetTaskByIdAsync(int id, string userId);
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId);
        Task<bool> DeleteOwnTaskAsync(int id, string userId);
        Task<bool> DeleteAnyTaskAsync(int id);
    }
}
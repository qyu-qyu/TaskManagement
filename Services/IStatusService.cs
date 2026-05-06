using TaskManagement.Models;

namespace TaskManagement.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetAllAsync();
        Task<Status?> GetByIdAsync(int id);
        Task<Status> CreateAsync(Status status);
        Task<bool> UpdateAsync(int id, Status status);
        Task<bool> DeleteAsync(int id);
    }
}
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface ITaskRepository
    {
       
        Task<TaskItem?> GetByIdAndUserIdAsync(int id, string userId);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        void Delete(TaskItem task);
        Task SaveChangesAsync();
        Task<List<TaskItem>> GetFilteredTasksAsync(
    string userId,
    int? statusId,
    int? priorityId,
    int pageNumber,
    int pageSize);
        Task<List<TaskItem>> GetOverdueTasksAsync(string userId);
    }
}
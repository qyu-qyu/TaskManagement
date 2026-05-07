using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface ICommentRepository
    {
        Task<List<TaskComment>> GetByTaskIdAsync(int taskId);
        Task<TaskComment?> GetByIdAsync(int id);
        Task AddAsync(TaskComment comment);
        void Delete(TaskComment comment);
        Task SaveChangesAsync();
    }
}
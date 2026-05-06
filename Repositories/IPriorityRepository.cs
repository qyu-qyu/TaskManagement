using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface IPriorityRepository
    {
        Task<List<Priority>> GetAllAsync();
        Task<Priority?> GetByIdAsync(int id);
        Task AddAsync(Priority priority);
        void Update(Priority priority);
        void Delete(Priority priority);
        Task SaveChangesAsync();
    }
}
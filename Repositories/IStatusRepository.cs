using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllAsync();
        Task<Status?> GetByIdAsync(int id);
        Task AddAsync(Status status);
        void Update(Status status);
        void Delete(Status status);
        Task SaveChangesAsync();
    }
}
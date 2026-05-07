using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetFilteredTasksAsync(
            string userId,
            int? statusId,
            int? priorityId,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .Where(t => t.CreatedByUserId == userId || t.AssignedToUserId == userId)
                .AsQueryable();

            if (statusId.HasValue)
                query = query.Where(t => t.StatusId == statusId.Value);

            if (priorityId.HasValue)
                query = query.Where(t => t.PriorityId == priorityId.Value);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAndUserIdAsync(int id, string userId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id &&
                    (t.CreatedByUserId == userId || t.AssignedToUserId == userId));
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> GetOverdueTasksAsync(string userId)
        {
            return await _context.Tasks
                .Include(t => t.Status)
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .Where(t =>
                    (t.CreatedByUserId == userId || t.AssignedToUserId == userId) &&
                    t.DueDate.HasValue &&
                    t.DueDate.Value < DateTime.UtcNow &&
                    t.Status.Name != "Completed")
                .ToListAsync();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskComment>> GetByTaskIdAsync(int taskId)
        {
            return await _context.TaskComments
                .Where(c => c.TaskItemId == taskId)
                .ToListAsync();
        }

        public async Task<TaskComment?> GetByIdAsync(int id)
        {
            return await _context.TaskComments.FindAsync(id);
        }

        public async Task AddAsync(TaskComment comment)
        {
            await _context.TaskComments.AddAsync(comment);
        }

        public void Delete(TaskComment comment)
        {
            _context.TaskComments.Remove(comment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
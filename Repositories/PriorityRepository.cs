using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly AppDbContext _context;

        public PriorityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Priority>> GetAllAsync()
        {
            return await _context.Priorities.ToListAsync();
        }

        public async Task<Priority?> GetByIdAsync(int id)
        {
            return await _context.Priorities.FindAsync(id);
        }

        public async Task AddAsync(Priority priority)
        {
            await _context.Priorities.AddAsync(priority);
        }

        public void Update(Priority priority)
        {
            _context.Priorities.Update(priority);
        }

        public void Delete(Priority priority)
        {
            _context.Priorities.Remove(priority);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
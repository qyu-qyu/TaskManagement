using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository;

        public PriorityService(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public async Task<List<Priority>> GetAllAsync()
        {
            return await _priorityRepository.GetAllAsync();
        }

        public async Task<Priority?> GetByIdAsync(int id)
        {
            return await _priorityRepository.GetByIdAsync(id);
        }

        public async Task<Priority> CreateAsync(Priority priority)
        {
            await _priorityRepository.AddAsync(priority);
            await _priorityRepository.SaveChangesAsync();

            return priority;
        }

        public async Task<bool> UpdateAsync(int id, Priority updatedPriority)
        {
            var priority = await _priorityRepository.GetByIdAsync(id);

            if (priority == null)
                return false;

            priority.Name = updatedPriority.Name;

            _priorityRepository.Update(priority);
            await _priorityRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var priority = await _priorityRepository.GetByIdAsync(id);

            if (priority == null)
                return false;

            _priorityRepository.Delete(priority);
            await _priorityRepository.SaveChangesAsync();

            return true;
        }
    }
}
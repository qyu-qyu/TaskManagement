using TaskManagement.DTOs;
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

        public async Task<List<PriorityResponseDto>> GetAllAsync()
        {
            var priorities = await _priorityRepository.GetAllAsync();

            return priorities.Select(p => new PriorityResponseDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public async Task<PriorityResponseDto?> GetByIdAsync(int id)
        {
            var priority = await _priorityRepository.GetByIdAsync(id);

            if (priority == null)
                return null;

            return new PriorityResponseDto
            {
                Id = priority.Id,
                Name = priority.Name
            };
        }

        public async Task<PriorityResponseDto> CreateAsync(PriorityDto dto)
        {
            var priority = new Priority
            {
                Name = dto.Name
            };

            await _priorityRepository.AddAsync(priority);
            await _priorityRepository.SaveChangesAsync();

            return new PriorityResponseDto
            {
                Id = priority.Id,
                Name = priority.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, PriorityDto dto)
        {
            var priority = await _priorityRepository.GetByIdAsync(id);

            if (priority == null)
                return false;

            priority.Name = dto.Name;

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
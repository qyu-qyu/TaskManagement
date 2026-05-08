using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<List<StatusResponseDto>> GetAllAsync()
        {
            var statuses = await _statusRepository.GetAllAsync();

            return statuses.Select(s => new StatusResponseDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }

        public async Task<StatusResponseDto?> GetByIdAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
                return null;

            return new StatusResponseDto
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public async Task<StatusResponseDto> CreateAsync(StatusDto dto)
        {
            var status = new Status
            {
                Name = dto.Name
            };

            await _statusRepository.AddAsync(status);
            await _statusRepository.SaveChangesAsync();

            return new StatusResponseDto
            {
                Id = status.Id,
                Name = status.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, StatusDto dto)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
                return false;

            status.Name = dto.Name;

            _statusRepository.Update(status);
            await _statusRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
                return false;

            _statusRepository.Delete(status);
            await _statusRepository.SaveChangesAsync();

            return true;
        }
    }
}
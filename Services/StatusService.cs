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

        public async Task<List<Status>> GetAllAsync()
        {
            return await _statusRepository.GetAllAsync();
        }

        public async Task<Status?> GetByIdAsync(int id)
        {
            return await _statusRepository.GetByIdAsync(id);
        }

        public async Task<Status> CreateAsync(Status status)
        {
            await _statusRepository.AddAsync(status);
            await _statusRepository.SaveChangesAsync();

            return status;
        }

        public async Task<bool> UpdateAsync(int id, Status updatedStatus)
        {
            var status = await _statusRepository.GetByIdAsync(id);

            if (status == null)
                return false;

            status.Name = updatedStatus.Name;

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
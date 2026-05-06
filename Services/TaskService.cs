using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskResponseDto>> GetAllTasksAsync(string userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                StatusId = t.StatusId,
                StatusName = t.Status?.Name,
                PriorityId = t.PriorityId,
                PriorityName = t.Priority?.Name,
                UserId = t.UserId
            }).ToList();
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);

            if (task == null)
                return null;

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                StatusId = task.StatusId,
                StatusName = task.Status?.Name,
                PriorityId = task.PriorityId,
                PriorityName = task.Priority?.Name,
                UserId = task.UserId
            };
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                StatusId = dto.StatusId,
                PriorityId = dto.PriorityId,
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                StatusId = task.StatusId,
                StatusName = task.Status?.Name,
                PriorityId = task.PriorityId,
                PriorityName = task.Priority?.Name,
                UserId = task.UserId
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);

            if (task == null)
                return false;

            if (dto.Title != null) task.Title = dto.Title;
            if (dto.Description != null) task.Description = dto.Description;
            if (dto.DueDate.HasValue) task.DueDate = dto.DueDate;
            if (dto.StatusId.HasValue) task.StatusId = dto.StatusId.Value;
            if (dto.PriorityId.HasValue) task.PriorityId = dto.PriorityId.Value;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOwnTaskAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);

            if (task == null)
                return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAnyTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null)
                return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }
    }
}
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

        public async Task<List<TaskResponseDto>> GetFilteredTasksAsync(
            string userId,
            int? statusId,
            int? priorityId,
            int pageNumber,
            int pageSize)
        {
            var tasks = await _taskRepository.GetFilteredTasksAsync(
                userId, statusId, priorityId, pageNumber, pageSize);

            return tasks.Select(t => MapToDto(t)).ToList();
        }

        public async Task<TaskResponseDto?> GetTaskByIdAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);
            if (task == null) return null;
            return MapToDto(task);
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
                CreatedByUserId = userId,
                AssignedToUserId = dto.AssignedToUserId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return MapToDto(task);
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);
            if (task == null) return false;

            if (dto.Title != null) task.Title = dto.Title;
            if (dto.Description != null) task.Description = dto.Description;
            if (dto.DueDate.HasValue) task.DueDate = dto.DueDate;
            if (dto.StatusId.HasValue) task.StatusId = dto.StatusId.Value;
            if (dto.PriorityId.HasValue) task.PriorityId = dto.PriorityId.Value;
            if (dto.AssignedToUserId != null) task.AssignedToUserId = dto.AssignedToUserId;
            if (dto.CategoryId.HasValue) task.CategoryId = dto.CategoryId.Value;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOwnTaskAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);
            if (task == null) return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAnyTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskResponseDto>> GetOverdueTasksAsync(string userId)
        {
            var tasks = await _taskRepository.GetOverdueTasksAsync(userId);
            return tasks.Select(t => MapToDto(t)).ToList();
        }

        private static TaskResponseDto MapToDto(TaskItem task)
        {
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
                CreatedByUserId = task.CreatedByUserId,
                AssignedToUserId = task.AssignedToUserId,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name
            };
        }
    }
}
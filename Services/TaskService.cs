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
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                Priority = t.Priority
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
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Priority = task.Priority
            };
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto, string userId)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                UserId = userId
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Priority = task.Priority
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto, string userId)
        {
            var task = await _taskRepository.GetByIdAndUserIdAsync(id, userId);

            if (task == null)
                return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = (bool)dto.IsCompleted;
            task.DueDate = dto.DueDate;
            task.Priority = dto.Priority;

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
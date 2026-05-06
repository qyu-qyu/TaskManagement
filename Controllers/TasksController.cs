using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private static TaskResponseDto MapToDto(TaskItem task) => new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            Priority = task.Priority,
            CreatedByUserId = task.CreatedByUserId,
            AssignedToUserId = task.AssignedToUserId,
            CategoryId = task.CategoryId,
            CategoryName = task.Category?.Name
        };

        // GET /api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetCurrentUserId();

            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .Where(t => t.CreatedByUserId == userId || t.AssignedToUserId == userId)
                .Select(t => MapToDto(t))
                .ToListAsync();

            return Ok(tasks);
        }

        // GET /api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();

            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id &&
                    (t.CreatedByUserId == userId || t.AssignedToUserId == userId));

            if (task == null)
                return NotFound(new { message = "Task not found." });

            return Ok(MapToDto(task));
        }

        // POST /api/tasks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                CreatedByUserId = userId,
                AssignedToUserId = dto.AssignedToUserId,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Reload with category
            await _context.Entry(task).Reference(t => t.Category).LoadAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, MapToDto(task));
        }

        // PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();

            var task = await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedByUserId == userId);

            if (task == null)
                return NotFound(new { message = "Task not found." });

            if (dto.Title != null) task.Title = dto.Title;
            if (dto.Description != null) task.Description = dto.Description;
            if (dto.IsCompleted.HasValue) task.IsCompleted = dto.IsCompleted.Value;
            if (dto.DueDate.HasValue) task.DueDate = dto.DueDate;
            if (dto.Priority != null) task.Priority = dto.Priority;
            if (dto.AssignedToUserId != null) task.AssignedToUserId = dto.AssignedToUserId;
            if (dto.CategoryId.HasValue) task.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return Ok(MapToDto(task));
        }

        // DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();

            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.CreatedByUserId == userId);

            if (task == null)
                return NotFound(new { message = "Task not found." });

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Task deleted successfully." });
        }
    }
}
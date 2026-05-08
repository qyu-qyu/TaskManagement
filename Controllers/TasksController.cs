using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int? statusId,
            int? priorityId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var userId = GetCurrentUserId();

            var tasks = await _taskService.GetFilteredTasksAsync(
                userId,
                statusId,
                priorityId,
                pageNumber,
                pageSize);

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.GetTaskByIdAsync(id, userId);

            if (task == null)
                return NotFound(new { message = "Task not found." });

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var task = await _taskService.CreateTaskAsync(dto, userId);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var updated = await _taskService.UpdateTaskAsync(id, dto, userId);

            if (!updated)
                return NotFound(new { message = "Task not found." });

            return Ok(new { message = "Task updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();

            if (User.IsInRole("Admin"))
            {
                var adminDeleted = await _taskService.DeleteAnyTaskAsync(id);

                if (!adminDeleted)
                    return NotFound(new { message = "Task not found." });

                return Ok(new { message = "Task deleted successfully." });
            }

            var deleted = await _taskService.DeleteOwnTaskAsync(id, userId);

            if (!deleted)
                return NotFound(new { message = "Task not found or access denied." });

            return Ok(new { message = "Task deleted successfully." });
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueTasks()
        {
            var userId = GetCurrentUserId();

            var tasks = await _taskService.GetOverdueTasksAsync(userId);

            return Ok(tasks);
        }
    }
}
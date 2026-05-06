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
    [Route("api/tasks/{taskId}/comments")]
    [Authorize]
    public class TaskCommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskCommentsController(AppDbContext context)
        {
            _context = context;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET /api/tasks/{taskId}/comments
        [HttpGet]
        public async Task<IActionResult> GetComments(int taskId)
        {
            var userId = GetCurrentUserId();

            // Check if task exists and user has access
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId &&
                    (t.CreatedByUserId == userId || t.AssignedToUserId == userId));

            if (task == null)
                return NotFound(new { message = "Task not found." });

            var comments = await _context.TaskComments
                .Where(c => c.TaskItemId == taskId)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt,
                    UserId = c.UserId,
                    TaskItemId = c.TaskItemId
                })
                .ToListAsync();

            return Ok(comments);
        }

        // POST /api/tasks/{taskId}/comments
        [HttpPost]
        public async Task<IActionResult> AddComment(int taskId, [FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();

            // Check if task exists and user has access
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId &&
                    (t.CreatedByUserId == userId || t.AssignedToUserId == userId));

            if (task == null)
                return NotFound(new { message = "Task not found." });

            var comment = new TaskComment
            {
                Content = dto.Content,
                TaskItemId = taskId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TaskComments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new CommentResponseDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId,
                TaskItemId = comment.TaskItemId
            });
        }

        // DELETE /api/tasks/{taskId}/comments/{commentId}
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int taskId, int commentId)
        {
            var userId = GetCurrentUserId();

            var comment = await _context.TaskComments
                .FirstOrDefaultAsync(c => c.Id == commentId &&
                    c.TaskItemId == taskId &&
                    c.UserId == userId);

            if (comment == null)
                return NotFound(new { message = "Comment not found." });

            _context.TaskComments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment deleted successfully." });
        }
    }
}
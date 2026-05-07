using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/tasks/{taskId}/comments")]
    [Authorize]
    public class TaskCommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public TaskCommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET /api/tasks/{taskId}/comments
        [HttpGet]
        public async Task<IActionResult> GetComments(int taskId)
        {
            var comments = await _commentService.GetByTaskIdAsync(taskId);
            return Ok(comments);
        }

        // POST /api/tasks/{taskId}/comments
        [HttpPost]
        public async Task<IActionResult> AddComment(int taskId, [FromBody] CreateCommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetCurrentUserId();
            var comment = await _commentService.AddAsync(taskId, userId, dto);
            return Ok(comment);
        }

        // DELETE /api/tasks/{taskId}/comments/{commentId}
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int taskId, int commentId)
        {
            var userId = GetCurrentUserId();
            var deleted = await _commentService.DeleteAsync(commentId, userId);

            if (!deleted)
                return NotFound(new { message = "Comment not found or unauthorized." });

            return Ok(new { message = "Comment deleted successfully." });
        }
    }
}
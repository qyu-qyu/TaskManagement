using TaskManagement.DTOs;
using TaskManagement.Models;
using TaskManagement.Repositories;

namespace TaskManagement.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentResponseDto>> GetByTaskIdAsync(int taskId)
        {
            var comments = await _commentRepository.GetByTaskIdAsync(taskId);
            return comments.Select(c => new CommentResponseDto
            {
                Id = c.Id,
                Content = c.Content,
                CreatedAt = c.CreatedAt,
                UserId = c.UserId,
                TaskItemId = c.TaskItemId
            }).ToList();
        }

        public async Task<CommentResponseDto> AddAsync(int taskId, string userId, CreateCommentDto dto)
        {
            var comment = new TaskComment
            {
                Content = dto.Content,
                TaskItemId = taskId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return new CommentResponseDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                UserId = comment.UserId,
                TaskItemId = comment.TaskItemId
            };
        }

        public async Task<bool> DeleteAsync(int commentId, string userId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null || comment.UserId != userId)
                return false;

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
            return true;
        }
    }
}
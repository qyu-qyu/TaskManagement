using TaskManagement.DTOs;

namespace TaskManagement.Services
{
    public interface ICommentService
    {
        Task<List<CommentResponseDto>> GetByTaskIdAsync(int taskId);
        Task<CommentResponseDto> AddAsync(int taskId, string userId, CreateCommentDto dto);
        Task<bool> DeleteAsync(int commentId, string userId);
    }
}
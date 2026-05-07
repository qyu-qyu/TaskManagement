using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int PriorityId { get; set; }

        // Assignment (our addition)
        public string? AssignedToUserId { get; set; }

        // Category (our addition)
        public int? CategoryId { get; set; }
    }

    public class UpdateTaskDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? StatusId { get; set; }

        public int? PriorityId { get; set; }

        // Assignment (our addition)
        public string? AssignedToUserId { get; set; }

        // Category (our addition)
        public int? CategoryId { get; set; }
    }

    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }

        // Assignment (our addition)
        public string CreatedByUserId { get; set; } = string.Empty;
        public string? AssignedToUserId { get; set; }

        // Category (our addition)
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    // Category DTOs
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    // Comment DTOs
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;
    }

    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
    }
}
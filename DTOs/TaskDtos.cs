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
        [RegularExpression("Low|Medium|High",
            ErrorMessage = "Priority must be Low, Medium, or High")]
        public string Priority { get; set; } = "Medium";

        public string? AssignedToUserId { get; set; }

        // Optional: assign a category
        public int? CategoryId { get; set; }
    }

    public class UpdateTaskDto
    {
        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        [RegularExpression("Low|Medium|High",
            ErrorMessage = "Priority must be Low, Medium, or High")]
        public string? Priority { get; set; }

        public string? AssignedToUserId { get; set; }

        // Optional: change category
        public int? CategoryId { get; set; }
    }

    public class TaskResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public string? AssignedToUserId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    // DTO for creating a category
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }

    // DTO for category response
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    // DTO for creating a comment
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; } = string.Empty;
    }

    // DTO for comment response
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
    }
}
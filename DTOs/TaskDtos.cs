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

        // Optional: assign task to another user
        public string? AssignedToUserId { get; set; }
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

        // Optional: reassign task to another user
        public string? AssignedToUserId { get; set; }
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
    }
}
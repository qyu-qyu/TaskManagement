using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        // Who created the task
        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        [ForeignKey("CreatedByUserId")]
        public ApplicationUser? CreatedByUser { get; set; }

        // Who the task is assigned to (optional)
        public string? AssignedToUserId { get; set; }

        [ForeignKey("AssignedToUserId")]
        public ApplicationUser? AssignedToUser { get; set; }

        // Status and Priority (relational - Member 1's work)
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;

        public int PriorityId { get; set; }
        public Priority Priority { get; set; } = null!;

        // Category (our addition)
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
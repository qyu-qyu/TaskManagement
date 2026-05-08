using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class PriorityDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }

    public class PriorityResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
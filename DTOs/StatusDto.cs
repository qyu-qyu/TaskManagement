using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTOs
{
    public class StatusDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }

    public class StatusResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
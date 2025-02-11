using System.ComponentModel.DataAnnotations;

namespace Taskr.API.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public List<TaskTag>? TaskTags { get; set; } = new();
    }
}

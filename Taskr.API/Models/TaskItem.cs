using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Taskr.API.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public TaskStatus Status { get; set; } = TaskStatus.Backlog;

        public List<TaskTag> TaskTags { get; set; } = new();
    }

    public enum TaskStatus
    {
        Backlog,
        ToDo,
        InProgress,
        Done
    }
}

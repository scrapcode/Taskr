namespace Taskr.API.Models
{
    public class TaskEditDto
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public TaskStatus Status { get; set; } = TaskStatus.Backlog;
    }
}

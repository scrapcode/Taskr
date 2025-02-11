namespace Taskr.API.Models
{
    public class TaskAddDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Tags { get; set; } = string.Empty;
    }
}

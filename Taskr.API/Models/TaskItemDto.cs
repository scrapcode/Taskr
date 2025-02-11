using System.ComponentModel.DataAnnotations;

namespace Taskr.API.Models
{
    public class TaskItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public List<TagDto>? Tags { get; set; } = new();
    }
}

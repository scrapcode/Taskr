using System.ComponentModel.DataAnnotations;

namespace Taskr.API.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public List<TaskTag> TaskTags { get; set; } = new();
    }
}

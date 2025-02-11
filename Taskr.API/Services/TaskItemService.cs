using Microsoft.EntityFrameworkCore;
using Taskr.API.Data;
using Taskr.API.Models;

namespace Taskr.API.Services
{
    public class TaskItemService(TaskrDbContext context)
    {
        private readonly TaskrDbContext _context = context;

        public async Task<TaskItem> AddTaskItemWithTagsAsync(string title, string content, List<string> tags)
        {
            var taskItem = new TaskItem
            {
                Title = title,
                Content = content
            };

            var normalizedTags = tags.Select(t => t.Trim().ToLower()).Distinct().ToList();

            // Get existing tags
            var existingTags = await _context.Tags
                .Where(t => normalizedTags.Contains(t.Name))
                .ToListAsync();

            // Determine which tags need to be created
            var newTagNames = normalizedTags.Except(existingTags.Select(t => t.Name)).ToList();

            var newTags = newTagNames.Select(name => new Tag { Name = name }).ToList();

            // Create new tags
            _context.Tags.AddRange(newTags);

            // Combine existing and new tags and add to the TaskItem
            taskItem.TaskTags = existingTags.Concat(newTags)
                .Select(t => new TaskTag { Tag = t, TaskItem = taskItem })
                .ToList();

            _context.TaskItems.Add(taskItem);

            await _context.SaveChangesAsync();

            return taskItem;
        }

    }
}

using TodoManager.DTOs.TodoItem;
using TodoManager.Models;

namespace TodoManager.Extensions
{
    public static class TodoItemMappingExtensions
    {
        // Método de extensão para mapear uma única entidade
        public static TodoItemDto MapToTodoItemDto(this TodoItem item)
        {
            if (item == null) return null;

            return new TodoItemDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Status = item.Status,
                DueDate = item.DueDate,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                UserName = item.User?.UserName
            };
        }

        public static IEnumerable<TodoItemDto> MapToTodoItemDto(this IEnumerable<TodoItem> items)
        {
            return items.Select(item => item.MapToTodoItemDto());
        }
    }
}

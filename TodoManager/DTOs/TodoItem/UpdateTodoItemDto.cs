using System.ComponentModel.DataAnnotations;
using TodoManager.Models;

namespace TodoManager.DTOs.TodoItem
{
    public class UpdateTodoItemDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        // O status é atualizável
        public TodoItemStatus Status { get; set; }
    }
}

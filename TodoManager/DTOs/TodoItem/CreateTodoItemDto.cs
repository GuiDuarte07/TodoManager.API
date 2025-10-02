using System.ComponentModel.DataAnnotations;

namespace TodoManager.DTOs.TodoItem
{
    public class CreateTodoItemDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        // Opcional
        public DateTime? DueDate { get; set; }

        // Status não é necessário na criação, pois o valor padrão é 'Pending'
    }
}

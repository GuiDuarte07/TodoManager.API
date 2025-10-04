using TodoManager.Models;

namespace TodoManager.Common
{
    public class TodoQueryParameters
    {
        // Paginação
        private const int MaxPageSize = 50; // Limite máximo suportado
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // Filtros
        public TodoItemStatus? Status { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; } 
        public string? SearchTerm { get; set; } 

        // Ordenação
        public string OrderBy { get; set; } = "CreatedAtDesc"; // Ex: "DueDateAsc", "TitleAsc"
    }
}

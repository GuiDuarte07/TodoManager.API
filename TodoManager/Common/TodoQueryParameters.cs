using TodoManager.Models;

namespace TodoManager.Common
{
    public class TodoQueryParameters
    {
        private const int MaxPageSize = 1000; // Limite máximo
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public TodoItemStatus? Status { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; } 
        public string? SearchTerm { get; set; } 

        public string OrderBy { get; set; } = "CreatedAtDesc"; // Ex: "DueDateAsc", "TitleAsc"
    }
}

namespace TodoManager.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoItemStatus Status { get; set; } = TodoItemStatus.Pending;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum TodoItemStatus
    {
        Pending,
        InProgress,
        Completed,
        Blocked
    }
}

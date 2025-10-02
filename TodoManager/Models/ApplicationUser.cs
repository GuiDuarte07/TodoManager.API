using Microsoft.AspNetCore.Identity;

namespace TodoManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}

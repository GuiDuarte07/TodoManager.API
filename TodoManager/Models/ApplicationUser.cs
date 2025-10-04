using Microsoft.AspNetCore.Identity;

namespace TodoManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}

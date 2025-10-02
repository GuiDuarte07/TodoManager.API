using Microsoft.EntityFrameworkCore;
using TodoManager.Data;
using TodoManager.Models;

namespace TodoManager.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetByIdAsync(int id, string userId)
        {
            return await _context.TodoItems
                .Include(t => t.User) // Inclui o usuário para o DTO
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync(string userId)
        {
            return await _context.TodoItems
                .Include(t => t.User)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task DeleteAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id, string userId)
        {
            return await _context.TodoItems.AnyAsync(t => t.Id == id && t.UserId == userId);
        }
    }
}

using TodoManager.Common;
using TodoManager.Models;

namespace TodoManager.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem?> GetByIdAsync(int id, string userId);
        Task<PagedList<TodoItem>> GetAllAsync(string userId, TodoQueryParameters parameters);
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task<TodoItem> UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(TodoItem todoItem);
        Task<bool> ExistsAsync(int id, string userId);
    }
}

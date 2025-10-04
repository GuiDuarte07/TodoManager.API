using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoManager.Common;
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

        public async Task<TodoItem?> GetByIdAsync(int id, string userId)
        {
            return await _context.TodoItems
                .Include(t => t.User) // Inclui o usuário para o DTO
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<PagedList<TodoItem>> GetAllAsync(string userId, TodoQueryParameters parameters)
        {
            var collection = _context.TodoItems
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .AsQueryable();

            if (parameters.Status.HasValue)
            {
                collection = collection.Where(t => t.Status == parameters.Status.Value);
            }

            if (parameters.DueDateFrom.HasValue)
            {
                collection = collection.Where(t => t.DueDate >= parameters.DueDateFrom.Value.Date);
            }

            if (parameters.DueDateTo.HasValue)
            {
                collection = collection.Where(t => t.DueDate <= parameters.DueDateTo.Value.Date.AddDays(1).AddSeconds(-1));
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var term = parameters.SearchTerm.Trim().ToLower();
                collection = collection.Where(t =>
                    t.Title.ToLower().Contains(term) ||
                    t.Description.ToLower().Contains(term));
            }

            collection = ApplySorting(collection, parameters.OrderBy);

            var totalCount = await collection.CountAsync();


            var items = await collection
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedList<TodoItem>
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        private IQueryable<TodoItem> ApplySorting(IQueryable<TodoItem> source, string orderBy)
        {
            return orderBy.ToLower() switch
            {
                "createdatasc" => source.OrderBy(t => t.CreatedAt),
                "titleasc" => source.OrderBy(t => t.Title),
                "duedateasc" => source.OrderBy(t => t.DueDate),
                "titledesc" => source.OrderByDescending(t => t.Title),
                "duedatedesc" => source.OrderByDescending(t => t.DueDate),
                _ => source.OrderByDescending(t => t.CreatedAt), // Padrão
            };
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

using TodoManager.Common;
using TodoManager.DTOs;
using TodoManager.DTOs.TodoItem;

namespace TodoManager.Services
{
    public interface ITodoItemService
    {
        Task<Result<TodoItemDto>> GetByIdAsync(int id, string userId);
        Task<Result<TodoItemDto>> CreateAsync(CreateTodoItemDto dto, string userId);
        Task<Result<TodoItemDto>> UpdateAsync(int id, UpdateTodoItemDto dto, string userId);
        Task<Result> DeleteAsync(int id, string userId);
        Task<Result<IPagedListDto<TodoItemDto>>> GetAllAsync(string userId, TodoQueryParameters parameters);

    }
}

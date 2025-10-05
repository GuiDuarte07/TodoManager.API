using TodoManager.Common;
using TodoManager.DTOs;
using TodoManager.DTOs.TodoItem;
using TodoManager.Extensions;
using TodoManager.Models;
using TodoManager.Repositories;

namespace TodoManager.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<TodoItemDto>> GetByIdAsync(int id, string userId)
        {
            var item = await _repository.GetByIdAsync(id, userId);

            if (item == null)
            {
                return Result<TodoItemDto>.Failure("Tarefa não encontrado ou sem permissão.");
            }

            return Result<TodoItemDto>.Success(item.MapToTodoItemDto());
        }

        public async Task<Result<TodoItemDto>> UpdateAsync(int id, UpdateTodoItemDto dto, string userId)
        {
            var existingItem = await _repository.GetByIdAsync(id, userId);

            if (existingItem == null)
            {
                return Result<TodoItemDto>.Failure("Tarefa não encontrado ou sem permissão.");
            }

            existingItem.Title = dto.Title;
            existingItem.Description = dto.Description;
            existingItem.DueDate = dto.DueDate;
            existingItem.Status = dto.Status;
            existingItem.UpdatedAt = DateTime.UtcNow;

            try
            {
                var updatedItem = await _repository.UpdateAsync(existingItem);
                return Result<TodoItemDto>.Success(updatedItem.MapToTodoItemDto());
            } catch (Exception ex) {
                return Result<TodoItemDto>.Failure(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id, string userId)
        {
            var itemToDelete = await _repository.GetByIdAsync(id, userId);

            if (itemToDelete == null)
            {
                return Result.Failure("Tarefa não encontrado ou sem permissão.");
            }

            try
            {
                await _repository.DeleteAsync(itemToDelete);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
            
        }

        public async Task<Result<IPagedListDto<TodoItemDto>>> GetAllAsync(string userId, TodoQueryParameters parameters)
        {
            var pagedList = await _repository.GetAllAsync(userId, parameters);

            var itemDtos = pagedList.Items.MapToTodoItemDto();

            var totalPages = (int)Math.Ceiling((double)pagedList.TotalCount / parameters.PageSize);

            var resultDto = new PagedListDto<TodoItemDto>
            {
                Items = itemDtos,
                TotalCount = pagedList.TotalCount,
                PageSize = parameters.PageSize,
                CurrentPage = parameters.PageNumber,
                TotalPages = totalPages
            };

            return Result<IPagedListDto<TodoItemDto>>.Success(resultDto);
        }

        public async Task<Result<TodoItemDto>> CreateAsync(CreateTodoItemDto dto, string userId)
        {
            var itemToCreate = new TodoItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                UserId = userId
            };

            try
            {
                var createdItem = await _repository.AddAsync(itemToCreate);
                return Result<TodoItemDto>.Success(createdItem.MapToTodoItemDto());
            }
            catch (Exception ex)
            {
                return Result<TodoItemDto>.Failure(ex.Message);
            }
        }
    }
}

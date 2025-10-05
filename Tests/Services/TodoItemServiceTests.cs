using Moq;
using TodoManager.DTOs.TodoItem;
using TodoManager.Models;
using TodoManager.Repositories;
using TodoManager.Services;
using TodoManager.Common;

namespace TodoManager.Tests.Services
{

    public class TodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _repositoryMock;
        private readonly TodoItemService _service;

        public TodoItemServiceTests()
        {
            _repositoryMock = new Mock<ITodoItemRepository>();
            _service = new TodoItemService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ItemExists_ReturnsSuccess()
        {
            var todo = new TodoItem { Id = 1, Title = "Test", UserId = "user1" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1"))
                .ReturnsAsync(todo);

            var result = await _service.GetByIdAsync(1, "user1");

            Assert.True(result.IsSuccess);
            Assert.Equal(todo.Title, result.Value.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ItemDoesNotExist_ReturnsFailure()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1"))
                .ReturnsAsync((TodoItem)null);

            var result = await _service.GetByIdAsync(1, "user1");

            Assert.False(result.IsSuccess);
            Assert.Equal("Tarefa não encontrado ou sem permissão.", result.Error);
        }

        [Fact]
        public async Task CreateAsync_ValidTodo_ReturnsSuccess()
        {
            var dto = new CreateTodoItemDto { Title = "New Task", Description = "Desc", DueDate = DateTime.UtcNow };
            var todo = new TodoItem { Id = 1, Title = dto.Title, UserId = "user1" };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(todo);

            var result = await _service.CreateAsync(dto, "user1");

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Title, result.Value.Title);
        }

        [Fact]
        public async Task UpdateAsync_ItemExists_ReturnsSuccess()
        {
            var existingTodo = new TodoItem { Id = 1, Title = "Old", UserId = "user1" };
            var dto = new UpdateTodoItemDto { Title = "Updated", Description = "Desc", DueDate = DateTime.UtcNow, Status = TodoItemStatus.Pending };

            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1")).ReturnsAsync(existingTodo);
            _repositoryMock.Setup(r => r.UpdateAsync(existingTodo)).ReturnsAsync(existingTodo);

            var result = await _service.UpdateAsync(1, dto, "user1");

            Assert.True(result.IsSuccess);
            Assert.Equal(dto.Title, result.Value.Title);
        }

        [Fact]
        public async Task UpdateAsync_ItemDoesNotExist_ReturnsFailure()
        {
            var dto = new UpdateTodoItemDto { Title = "Updated" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1")).ReturnsAsync((TodoItem)null);

            var result = await _service.UpdateAsync(1, dto, "user1");

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_ItemExists_ReturnsSuccess()
        {
            var todo = new TodoItem { Id = 1, UserId = "user1" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1")).ReturnsAsync(todo);
            _repositoryMock.Setup(r => r.DeleteAsync(todo)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(1, "user1");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task DeleteAsync_ItemDoesNotExist_ReturnsFailure()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(1, "user1")).ReturnsAsync((TodoItem)null);

            var result = await _service.DeleteAsync(1, "user1");

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsPagedList()
        {
            var todoList = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Task1", UserId = "user1" },
            new TodoItem { Id = 2, Title = "Task2", UserId = "user1" }
        };

            var pagedList = new PagedList<TodoItem>
            {
                TotalCount = 2,
                Items = todoList
            };

            var parameters = new TodoQueryParameters { PageNumber = 1, PageSize = 10 };

            _repositoryMock.Setup(r => r.GetAllAsync("user1", parameters))
                .ReturnsAsync(pagedList);

            var result = await _service.GetAllAsync("user1", parameters);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Items.Count());
        }
    }

}
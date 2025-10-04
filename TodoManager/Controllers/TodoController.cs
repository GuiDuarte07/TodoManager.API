using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Text.Json;
using TodoManager.Common;
using TodoManager.DTOs.TodoItem;
using TodoManager.DTOs;
using System.Security.Claims;
using TodoManager.Services;

namespace TodoManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly ILogger<TodoController> _logger;
        private readonly ITodoItemService _service;

        public TodoController(ILogger<TodoController> logger, ITodoItemService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> Get(int id)
        {
            var userId = GetUserId();
            var result = await _service.GetByIdAsync(id, userId);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<ActionResult<IPagedListDto<TodoItemDto>>> Get([FromQuery] TodoQueryParameters parameters)
        {
            var userId = GetUserId();
            var result = await _service.GetAllAsync(userId, parameters);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Post([FromBody] CreateTodoItemDto dto)
        {
            var userId = GetUserId();
            var result = await _service.CreateAsync(dto, userId);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.Error });
            }

            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTodoItemDto dto)
        {
            var userId = GetUserId();
            var result = await _service.UpdateAsync(id, dto, userId);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var result = await _service.DeleteAsync(id, userId);

            if (!result.IsSuccess)
            {
                return NotFound(new { error = result.Error });
            }

            return NoContent();
        }

        private string GetUserId()
        {
            // Obtém o ID do usuário (Identity ID) a partir do token JWT
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using System.Data.Entity.Core;

namespace TodoAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemModelsController : ControllerBase
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemModelsController(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItemModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemModel>>> GetToDos()
        {
            try
            {
                return new ActionResult<IEnumerable<TodoItemModel>>(await _repository.GetAllItems());
            }
            catch (ObjectNotFoundException)
            {

                return NotFound();
            }
        }

        // GET: api/TodoItemModels/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemModel>> GetTodoItemModel(int id)
        {
            try
            {
                return await _repository.GetById(id);
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/TodoItemModels/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemModel(int id, TodoItemModel todoItemModel)
        {
            try
            {
                await _repository.ModifyItemById(id, todoItemModel);
            }
            catch (Exception e)
            {
                if (e is ObjectNotFoundException)
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItemModels
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> PostTodoItemModel(TodoItemModel todoItemModel)
        {
            try
            {
                await _repository.CreateNewItem(todoItemModel);
            }
            catch (Exception)
            {

                return Problem("Entity set 'ApplicationDbContext.ToDos'  is null.");
            }

            return CreatedAtAction("GetTodoItemModel", new { id = todoItemModel.Id }, todoItemModel);
        }

        // DELETE: api/TodoItemModels/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemModel(int id)
        {
            try
            {
                await _repository.DeleteItemById(id);
            }
            catch (ObjectNotFoundException)
            {

                return NotFound(id);
            }

            return NoContent();
        }
    }
}

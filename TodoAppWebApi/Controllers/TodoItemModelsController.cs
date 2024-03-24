using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Exceptions;
using TodoAppWebApi.Validators;

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
            catch (DbSetNotFoundException)
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
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/TodoItemModels/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemModel(int id, TodoItemModel todoItemModel)
        {
            var errors = ValidateTodoItem(todoItemModel);

            if (errors.Count > 0)
            {
                return Ok(errors);
            }

            try
            {
                await _repository.ModifyItemById(id, todoItemModel);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case EntityNotFoundException:
                        return NotFound();
                    case KeyMismatchException:
                    default:
                        return BadRequest();
                }
            }

            return NoContent();
        }

        // POST: api/TodoItemModels
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> PostTodoItemModel(TodoItemModel todoItemModel)
        {
            var errors = ValidateTodoItem(todoItemModel);

            if (errors.Count > 0)
            {
                return Ok(errors);
            }

            try
            {
                await _repository.CreateNewItem(todoItemModel);
            }
            catch (DbSetNotFoundException)
            {

                return NotFound();
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
            catch (EntityNotFoundException)
            {

                return NotFound(id);
            }

            return NoContent();
        }

        private List<ValidationError> ValidateTodoItem(TodoItemModel todoItemModel)
        {
            var validator = new TodoItemValidator();
            var errors = validator.Validate(todoItemModel);

            return errors;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using DataAccess.Entities;

namespace TodoAppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemModelsController : ControllerBase
    {
        private readonly IApplicationDbContext _dbContext;

        public TodoItemModelsController(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        // GET: api/TodoItemModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemModel>>> GetToDos()
        {
          if (_dbContext.ToDos == null)
          {
              return NotFound();
          }
            return await _dbContext.ToDos.ToListAsync();
        }

        // GET: api/TodoItemModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemModel>> GetTodoItemModel(int id)
        {
          if (_dbContext.ToDos == null)
          {
              return NotFound();
          }
            var todoItemModel = await _dbContext.ToDos.FindAsync(id);

            if (todoItemModel == null)
            {
                return NotFound();
            }

            return todoItemModel;
        }

        // PUT: api/TodoItemModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemModel(int id, TodoItemModel todoItemModel)
        {
            if (id != todoItemModel.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(todoItemModel).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItemModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> PostTodoItemModel(TodoItemModel todoItemModel)
        {
          if (_dbContext.ToDos == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ToDos'  is null.");
          }
            _dbContext.ToDos.Add(todoItemModel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetTodoItemModel", new { id = todoItemModel.Id }, todoItemModel);
        }

        // DELETE: api/TodoItemModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemModel(int id)
        {
            if (_dbContext.ToDos == null)
            {
                return NotFound();
            }
            var todoItemModel = await _dbContext.ToDos.FindAsync(id);
            if (todoItemModel == null)
            {
                return NotFound();
            }

            _dbContext.ToDos.Remove(todoItemModel);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemModelExists(int id)
        {
            return (_dbContext.ToDos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

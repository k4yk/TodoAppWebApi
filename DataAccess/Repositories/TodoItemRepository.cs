using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core;

namespace DataAccess.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TodoItemRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItemModel>> GetAllItems()
        {
            if (_dbContext.ToDos == null)
            {
                throw new DbSetNotFoundException();
            }
            return await _dbContext.ToDos.ToListAsync();
        }

        public async Task<TodoItemModel> GetById(int id)
        {
            if (_dbContext.ToDos == null)
            {
                throw new DbSetNotFoundException();
            }
            var todoItemModel = await _dbContext.ToDos.FindAsync(id);

            if (todoItemModel == null)
            {
                throw new EntityNotFoundException();
            }

            return todoItemModel;
        }

        public async Task ModifyItemById(int id, TodoItemModel todoItemModel)
        {
            if (id != todoItemModel.Id)
            {
                throw new KeyMismatchException();
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
                    throw new EntityNotFoundException();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<TodoItemModel> CreateNewItem(TodoItemModel todoItemModel)
        {
            if (_dbContext.ToDos == null)
            {
                throw new DbSetNotFoundException();
            }
            _dbContext.ToDos.Add(todoItemModel);
            await _dbContext.SaveChangesAsync();

            return todoItemModel;
        }

        public async Task DeleteItemById(int id)
        {
            if (_dbContext.ToDos == null)
            {
                throw new DbSetNotFoundException();
            }
            var todoItemModel = await _dbContext.ToDos.FindAsync(id);
            if (todoItemModel == null)
            {
                throw new EntityNotFoundException();
            }

            _dbContext.ToDos.Remove(todoItemModel);
            await _dbContext.SaveChangesAsync();
        }

        private bool TodoItemModelExists(int id)
        {
            return (_dbContext.ToDos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}

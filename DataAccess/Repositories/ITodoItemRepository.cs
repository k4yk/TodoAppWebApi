using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItemModel> CreateNewItem(TodoItemModel todoItemModel);
        Task DeleteItemById(int id);
        Task<IEnumerable<TodoItemModel>> GetAllItems();
        Task<TodoItemModel> GetById(int id);
        Task ModifyItemById(int id, TodoItemModel todoItemModel);
    }
}
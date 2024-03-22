namespace DataAccess.Entities
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsDone { get; set; } = false;

    }
}

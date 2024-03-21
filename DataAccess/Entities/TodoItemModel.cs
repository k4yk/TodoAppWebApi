namespace DataAccess.Entities
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(7);

    }
}

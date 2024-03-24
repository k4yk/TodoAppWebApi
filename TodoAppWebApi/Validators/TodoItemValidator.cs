using DataAccess.Entities;

namespace TodoAppWebApi.Validators
{
    public class TodoItemValidator
    {
        private readonly List<ValidationError> _errors = new List<ValidationError>();

        public List<ValidationError> Validate(TodoItemModel item)
        {
            if (string.IsNullOrEmpty(item.Title))
            {
                _errors.Add(new ValidationError("Title", "Title is required!"));
            }
            if (string.IsNullOrEmpty(item.Description))
            {
                _errors.Add(new ValidationError("Description", "Description is required"));
            }
            return _errors;
        }
    }
}

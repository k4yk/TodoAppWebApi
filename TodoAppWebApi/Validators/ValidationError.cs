namespace TodoAppWebApi.Validators
{
    public class ValidationError
    {
        public ValidationError(string type, string description)
        {
            Type = type;
            Description = description;
        }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}

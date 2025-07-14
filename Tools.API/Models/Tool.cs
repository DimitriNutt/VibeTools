namespace Tools.API.Models
{
    public class Tool
    {
        public Guid Id { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public IList<string> Category { get; set; } = [];
    }
}

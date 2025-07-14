namespace Tools.API.Models
{
    public class Review
    {
        public Guid Id { get; set; } = new();
        public Guid ToolId { get; set; }
        public double Rating { get; set; } = 0;
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ReviewerName { get; set; } = "Anonymous";
    }
}
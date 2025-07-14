namespace Tools.API.Models.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? ReviewerName { get; set; }
    }
}
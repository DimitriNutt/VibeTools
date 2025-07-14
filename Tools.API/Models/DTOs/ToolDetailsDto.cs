namespace Tools.API.Models.DTOs
{
    public class ToolDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<string> Category { get; set; } = [];
        public double AverageRating { get; set; }
        public bool IsCommunityFavorite { get; set; }
        public bool IsHidden { get; set; }
        public IList<ReviewDto> Reviews { get; set; } = [];
    }
}
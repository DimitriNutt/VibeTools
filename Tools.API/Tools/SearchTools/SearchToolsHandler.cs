namespace Tools.API.Tools.SearchTools
{
    public record SearchToolsQuery(string? Name, string? Category, string? Description, int? PageNumber = 1, int? PageSize = 10) : IQuery<SearchToolsResult>;
    public record SearchToolsResult(IEnumerable<ToolDetailsDto> Tools);

    internal class SearchToolsQueryHandler(IDocumentSession session) : IQueryHandler<SearchToolsQuery, SearchToolsResult>
    {
        public async Task<SearchToolsResult> Handle(SearchToolsQuery query, CancellationToken cancellationToken)
        {
            var toolsQuery = session.Query<Tool>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
                toolsQuery = toolsQuery.Where(t => t.Name.Contains(query.Name, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(query.Category))
                toolsQuery = toolsQuery.Where(t => t.Category.Any(c => c.Equals(query.Category, StringComparison.CurrentCultureIgnoreCase)));

            if (!string.IsNullOrWhiteSpace(query.Description))
                toolsQuery = toolsQuery.Where(t => t.Description.Contains(query.Description, StringComparison.CurrentCultureIgnoreCase));

            var tools = await toolsQuery.ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
            var toolIds = tools.Select(t => t.Id).ToList();

            var reviews = await session.Query<Review>()
                .Where(r => toolIds.Contains(r.ToolId))
                .ToListAsync(cancellationToken);

            var toolDetails = tools.Select(tool =>
            {
                var toolReviews = reviews.Where(r => r.ToolId == tool.Id).OrderByDescending(r => r.CreatedAt).ToList();
                var averageRating = toolReviews.Any() ? toolReviews.Average(r => r.Rating) : 0;
                var latestFive = toolReviews.Take(5).ToList();
                var isCommunityFavorite = latestFive.Count == 5 && latestFive.All(r => r.Rating == 5);
                var isHidden = latestFive.Count == 5 && latestFive.All(r => r.Rating == 1);

                return new ToolDetailsDto
                {
                    Id = tool.Id,
                    Name = tool.Name,
                    Description = tool.Description,
                    Category = tool.Category,
                    AverageRating = averageRating,
                    IsCommunityFavorite = isCommunityFavorite,
                    IsHidden = isHidden,
                    Reviews = [.. toolReviews.Select(r => new ReviewDto
                    {
                        Id = r.Id,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        CreatedAt = r.CreatedAt,
                        ReviewerName = r.ReviewerName
                    })]
                };
            }).ToList();

            return new SearchToolsResult(toolDetails);
        }
    }
}

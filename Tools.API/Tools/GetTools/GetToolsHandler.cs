namespace Tools.API.Tools.GetTools
{
    public record GetToolsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetToolsResult>;
    public record GetToolsResult(IEnumerable<ToolDetailsDto> Tools);

    internal class GetToolsQueryHandler(IDocumentSession session) : IQueryHandler<GetToolsQuery, GetToolsResult>
    {
        public async Task<GetToolsResult> Handle(GetToolsQuery query, CancellationToken cancellationToken)
        {
            var tools = await session.Query<Tool>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
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
            }).OrderByDescending(t => t.AverageRating).ToList();

            return new GetToolsResult(toolDetails);
        }
    }
}

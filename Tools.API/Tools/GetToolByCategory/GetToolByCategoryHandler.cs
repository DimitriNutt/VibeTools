namespace Tools.API.Tools.GetToolByCategory
{
    public record GetToolByCategoryQuery(string Category) : IQuery<GetToolByCategoryResult>;
    public record GetToolByCategoryResult(IEnumerable<ToolDetailsDto> Tools);

    internal class GetToolByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetToolByCategoryQuery, GetToolByCategoryResult>
    {
        public async Task<GetToolByCategoryResult> Handle(GetToolByCategoryQuery query, CancellationToken cancellationToken)
        {
            var tools = await session.Query<Tool>()
            .Where(p => p.Category.Any(c => c.Contains(query.Category, StringComparison.CurrentCultureIgnoreCase)))
            .ToListAsync(token: cancellationToken);

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

            return new GetToolByCategoryResult(toolDetails);
        }
    }
}
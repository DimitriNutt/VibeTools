namespace Tools.API.Tools.GetToolById
{
    public record GetToolByIdQuery(Guid Id) : IQuery<GetToolByIdResult>;
    public record GetToolByIdResult(ToolDetailsDto Tool);

    public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetToolByIdQuery, GetToolByIdResult>
    {
        public async Task<GetToolByIdResult> Handle(GetToolByIdQuery query, CancellationToken cancellationToken)
        {
            var tool = await session.LoadAsync<Tool>(query.Id, cancellationToken);
            if (tool is null) throw new ToolNotFoundException(query.Id);


            var reviews = await session.Query<Review>()
                .Where(r => r.ToolId == tool.Id)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);

            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
            var latestFive = reviews.Take(5).ToList();
            var isCommunityFavorite = latestFive.Count == 5 && latestFive.All(r => r.Rating == 5);
            var isHidden = latestFive.Count == 5 && latestFive.All(r => r.Rating == 1);

            var toolDetails = new ToolDetailsDto
            {
                Id = tool.Id,
                Name = tool.Name,
                Description = tool.Description,
                Category = tool.Category,
                AverageRating = averageRating,
                IsCommunityFavorite = isCommunityFavorite,
                IsHidden = isHidden,
                Reviews = [.. reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    ReviewerName = r.ReviewerName
                })]
            };

            return new GetToolByIdResult(toolDetails);
        }
    }
}

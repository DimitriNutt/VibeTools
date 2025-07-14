namespace Tools.API.Reviews.CreateReview
{
    public record CreateReviewCommand(int Rating, string Comment, Guid ToolId) : ICommand<CreateReviewResult>;
    public record CreateReviewResult(Guid Id);

    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required");
            RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required");
        }
    }

    internal class CreateReviewCommandHandler(IDocumentSession session) : ICommandHandler<CreateReviewCommand, CreateReviewResult>
    {
        public async Task<CreateReviewResult> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                Comment = command.Comment,
                Rating = command.Rating,
                ToolId = command.ToolId,
            };

            session.Store(review);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateReviewResult(review.Id);
        }
    }
}

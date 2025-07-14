namespace Tools.API.Tools.CreateTool
{
    public record CreateToolCommand(string Name, IList<string> Category, string Description) : ICommand<CreateToolResult>;
    public record CreateToolResult(Guid Id);

    public class CreateToolCommandValidator : AbstractValidator<CreateToolCommand>
    {
        public CreateToolCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        }
    }

    internal class CreateToolCommandHandler(IDocumentSession session) : ICommandHandler<CreateToolCommand, CreateToolResult>
    {
        public async Task<CreateToolResult> Handle(CreateToolCommand command, CancellationToken cancellationToken)
        {
            var tool = new Tool
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
            };

            session.Store(tool);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateToolResult(tool.Id);
        }
    }
}

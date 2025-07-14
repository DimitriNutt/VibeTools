namespace Tools.API.Tools.UpdateTools
{
    public record UpdateToolCommand(Guid Id, string Name, IList<string> Category, string Description) : ICommand<UpdateToolResult>;
    public record UpdateToolResult(bool IsSuccess);

    public class UpdateToolCommandValidator : AbstractValidator<UpdateToolCommand>
    {
        public UpdateToolCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Tool ID is required");
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required").Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        }
    }

    internal class UpdateToolCommandHandler(IDocumentSession session) : ICommandHandler<UpdateToolCommand, UpdateToolResult>
    {
        public async Task<UpdateToolResult> Handle(UpdateToolCommand command, CancellationToken cancellationToken)
        {
            var tool = await session.LoadAsync<Tool>(command.Id, cancellationToken);

            if (tool is null)
            {
                throw new ToolNotFoundException(command.Id);
            }

            tool.Name = command.Name;
            tool.Category = command.Category;
            tool.Description = command.Description;

            session.Update(tool);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateToolResult(true);
        }
    }
}
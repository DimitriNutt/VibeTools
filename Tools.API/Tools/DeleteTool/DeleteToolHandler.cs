namespace Tools.API.Tools.DeleteTool
{
    public record DeleteToolCommand(Guid Id) : ICommand<DeleteToolResult>;
    public record DeleteToolResult(bool IsSuccess);

    public class DeleteToolCommandValidator : AbstractValidator<DeleteToolCommand>
    {
        public DeleteToolCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Tool ID is required");
        }
    }

    internal class DeleteToolCommandHandler(IDocumentSession session) : ICommandHandler<DeleteToolCommand, DeleteToolResult>
    {
        public async Task<DeleteToolResult> Handle(DeleteToolCommand command, CancellationToken cancellationToken)
        {
            session.Delete<Tool>(command.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteToolResult(true);
        }
    }
}
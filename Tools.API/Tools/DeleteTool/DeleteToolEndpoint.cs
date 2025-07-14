namespace Tools.API.Tools.DeleteTool
{
    public record DeleteToolResponse(bool IsSuccess);

    public class DeleteToolEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/tool/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteToolCommand(id));

                var response = result.Adapt<DeleteToolResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteTool")
                .Produces<DeleteToolResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Tool")
                .WithDescription("Delete Tool");
        }
    }
}
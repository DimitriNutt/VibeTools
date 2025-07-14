namespace Tools.API.Tools.UpdateTools
{
    public record UpdateToolRequest(Guid Id, string Name, IList<string> Category, string Description);
    public record UpdateToolResponse(bool IsSuccess);

    public class UpdateToolEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/tools", async (UpdateToolRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateToolCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateToolResponse>();

                return Results.Ok(response);
            })
                .WithName("UpdateTool")
                .Produces<UpdateToolResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Tool")
                .WithDescription("Update Tool");
        }
    }
}

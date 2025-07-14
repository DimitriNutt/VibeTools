namespace Tools.API.Tools.CreateTool
{
    public record CreateToolRequest(string Name, IList<string> Category, string Description);
    public record CreateToolResponse(Guid Id);

    public class CreateToolEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/tools", async (CreateToolRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateToolCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateToolResponse>();

                return Results.Created($"/tools/{response.Id}", response);
            })
                .WithName("CreateTool")
                .Produces<CreateToolResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Tool")
                .WithDescription("Create Tool");
        }
    }
}

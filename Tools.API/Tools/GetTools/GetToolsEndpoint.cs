namespace Tools.API.Tools.GetTools
{
    public record GetToolsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetToolsResponse(IEnumerable<ToolDetailsDto> Tools);

    public class GetToolsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/tools", async ([AsParameters] GetToolsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetToolsQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetToolsResponse>();

                return Results.Ok(response);
            })
                .WithName("GetTools")
                .Produces<GetToolsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Tools")
                .WithDescription("Get Tools");
        }
    }
}
namespace Tools.API.Tools.SearchTools
{
    public record SearchToolsRequest(string? Name, string? Category, string? Description, int? PageNumber = 1, int? PageSize = 10);
    public record SearchToolsResponse(IEnumerable<ToolDetailsDto> Tools);

    public class SearchToolsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/tools/search", async ([AsParameters] SearchToolsRequest request, ISender sender) =>
            {

                var query = request.Adapt<SearchToolsQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<SearchToolsResponse>();

                return Results.Ok(response);
            })
            .WithName("SearchTools")
            .Produces<IList<ToolDetailsDto>>(StatusCodes.Status200OK)
            .WithSummary("Search Tools")
            .WithDescription("Search Tools");
        }
    }
}
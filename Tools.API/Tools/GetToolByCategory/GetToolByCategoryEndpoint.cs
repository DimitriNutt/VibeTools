namespace Tools.API.Tools.GetToolByCategory
{
    public record GetToolByCategoryResponse(IEnumerable<ToolDetailsDto> Tools);

    public class GetToolByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("tools/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetToolByCategoryQuery(category));
                var respose = result.Adapt<GetToolByCategoryResponse>();
                return Results.Ok(respose);
            })
                .WithName("GetToolByCategory")
                .Produces<GetToolByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Tool By Category")
                .WithDescription("Get Tool By Category");
        }
    }
}
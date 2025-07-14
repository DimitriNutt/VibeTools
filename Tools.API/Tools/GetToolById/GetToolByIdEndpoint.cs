namespace Tools.API.Tools.GetToolById
{
    public record GetToolByIdResponse(ToolDetailsDto Tool);

    public class GetToolByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/tools/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetToolByIdQuery(id));

                var response = result.Adapt<GetToolByIdResponse>();

                return Results.Ok(response);
            })
                .WithName("GetToolById")
                .Produces<GetToolByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Tool By Id")
                .WithDescription("Get Tool By Id");
        }
    }
}
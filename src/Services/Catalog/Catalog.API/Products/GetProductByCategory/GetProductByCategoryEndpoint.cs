namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var product = await sender.Send(new GetProductByCategoryQuery(category));
            var response = product.Adapt<GetProductByCategoryResult>();
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .WithSummary("Get product by category")
        .WithDescription("Get product by category")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
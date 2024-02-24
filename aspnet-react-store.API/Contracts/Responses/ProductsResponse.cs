namespace aspnet_react_store.API.Contracts.Responses
{
    public record ProductsResponse(
        int Id,
        string Name,
        decimal Price,
        int Quantity,
        decimal Discount,
        string? Description,
        CategoriesResponse[]? Categories,
        ImagesResponse[]? Images);
}

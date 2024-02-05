namespace aspnet_react_store.API.Contracts
{
    public record ProductsResponse(
        int Id,
        string Name,
        decimal Price,
        string? Description,
        ImagesResponse[]? Images);
}

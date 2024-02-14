namespace aspnet_react_store.API.Contracts.Responses
{
    public record ProductsResponse(
        int Id,
        string Name,
        decimal Price,
        string? Description,
        ImagesResponse[]? Images);
}

namespace aspnet_react_store.API.Contracts.Responses
{
    public record CartProductsResponse(
        int Id,
        string Name,
        decimal Price,
        int Quantity,
        int QuantityInCart,
        decimal Discount,
        ImagesResponse? Image
    );
}
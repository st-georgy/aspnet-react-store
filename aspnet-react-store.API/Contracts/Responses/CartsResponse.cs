namespace aspnet_react_store.API.Contracts.Responses
{
    public record CartsResponse(
        int TotalProducts,
        decimal TotalPrice,
        decimal Discount,
        CartProductsResponse[]? Products
    );
}
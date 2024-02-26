namespace aspnet_react_store.Persistence.Entities.Linking
{
    public class ProductCartEntity
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int CartId { get; set; }
        public CartEntity Cart { get; set; } = null!;
    }
}
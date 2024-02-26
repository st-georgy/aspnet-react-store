namespace aspnet_react_store.Persistence.Entities.Linking
{
    public class ProductOrderEntity
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;
    }
}
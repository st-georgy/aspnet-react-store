namespace aspnet_react_store.Persistence.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<ImageEntity> Images { get; set; } = [];
        public List<CartEntity> Carts { get; set; } = [];
        public List<OrderEntity> Orders { get; set; } = [];
    }
}
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public string? Description { get; set; }

        public List<ProductCartEntity> ProductCarts { get; set; } = [];
        public List<CategoryEntity> Categories { get; set; } = [];
        public List<ImageEntity> Images { get; set; } = [];
        public List<ProductOrderEntity> ProductOrders { get; set; } = [];
        public List<UserEntity> Users { get; set; } = [];
    }
}
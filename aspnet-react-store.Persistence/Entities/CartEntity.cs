namespace aspnet_react_store.Persistence.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalProducts { get; set; }
        public decimal Discount { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public List<ProductEntity> Products { get; set; } = [];
    }
}
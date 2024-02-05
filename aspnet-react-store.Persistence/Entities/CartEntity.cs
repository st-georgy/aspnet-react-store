namespace aspnet_react_store.Persistence.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserEntity? User { get; set; }

        public List<ProductEntity> Products { get; set; } = [];
    }
}
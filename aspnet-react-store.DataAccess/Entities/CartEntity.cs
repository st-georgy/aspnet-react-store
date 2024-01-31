namespace aspnet_react_store.DataAccess.Entities
{
    public class CartEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserEntity? User { get; set; }

        public List<ProductEntity> Products { get; set; } = [];
    }
}
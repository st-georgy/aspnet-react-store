namespace aspnet_react_store.Persistence.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<ProductEntity> Products { get; set; } = [];
    }
}
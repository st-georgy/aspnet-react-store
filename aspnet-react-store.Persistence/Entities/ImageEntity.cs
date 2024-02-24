namespace aspnet_react_store.Persistence.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = null!;
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
    }
}
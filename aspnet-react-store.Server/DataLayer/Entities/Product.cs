namespace aspnet_react_store.Server.Entities {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PicturesPath { get; set; }

        public List<Cart> Carts { get; set; } = [];
        public List<Order> Orders { get; set; } = [];
    }
}
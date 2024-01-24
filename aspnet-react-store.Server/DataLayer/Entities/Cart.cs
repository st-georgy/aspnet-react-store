namespace aspnet_react_store.Server.Entities {
    public class Cart {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Product> Products { get; set; } = [];
    }    
}
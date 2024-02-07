namespace aspnet_react_store.Domain.Abstractions.Auth
{
    public interface IPasswordHashProvider
    {
        public string Generate(string password);
        public bool Verify(string password, string hashedPassword);
    }
}

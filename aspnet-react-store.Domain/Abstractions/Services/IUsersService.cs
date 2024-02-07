namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IUsersService
    {
        Task<string> Login(string email, string password);
        Task Register(string username, string email, string password);
    }
}
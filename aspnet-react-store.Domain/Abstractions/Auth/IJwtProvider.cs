using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Auth
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}

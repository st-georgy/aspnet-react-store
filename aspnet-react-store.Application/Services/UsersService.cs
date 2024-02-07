using aspnet_react_store.Domain.Abstractions.Auth;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Application.Services
{
    public class UsersService(
        IUsersRepository usersRepository,
        IPasswordHashProvider passwordHasher,
        IJwtProvider jwtProvider) : IUsersService
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly IPasswordHashProvider _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
                throw new Exception("Failed to login");

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task Register(string username, string email, string password)
        {
            var passwordHash = _passwordHasher.Generate(password);


            var user = User.Create(
                await _usersRepository.GetNextId(), username, email, passwordHash, null, null
            );

            if (user.IsSuccess)
                await _usersRepository.Add(user.Value);
            else
                throw new Exception("Failed to register: " + user.Error);
        }
    }
}

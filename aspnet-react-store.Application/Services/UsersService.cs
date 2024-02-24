using aspnet_react_store.Domain.Abstractions.Auth;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Exceptions;
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

        public async Task<User> GetUserById(int id) =>
            await _usersRepository.GetById(id);

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
                throw new AuthorizationFailedException("Failed to login");

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task Register(string username, string email, string password)
        {
            var passwordHash = _passwordHasher.Generate(password);

            var user = User.Create(
                0, username, email, passwordHash, null, null, [], []
            );

            if (user.IsSuccess)
                await _usersRepository.Create(user.Value);
            else
                throw new RegisterFailedException(user.Error);
        }

        public async Task<int> UpdateUser(int id, string? username, string? email) =>
            await _usersRepository.Update(id, username, email);

        public async Task<int> UpdatePassword(int id, string oldPassword, string newPassword)
        {
            var oldPasswordHash = _passwordHasher.Generate(oldPassword);

            var user = await _usersRepository.GetById(id);

            if (user.PasswordHash != oldPasswordHash)
                throw new AuthorizationFailedException("Old password is incorrect.");

            var newPasswordHash = _passwordHasher.Generate(newPassword);

            await _usersRepository.UpdatePassword(id, newPasswordHash);

            return id;
        }
    }
}

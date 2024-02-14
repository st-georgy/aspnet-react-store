using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Application.Services
{
    public class UserInfosService(IUserInfosRepository userInfosRepository) : IUserInfosService
    {
        private readonly IUserInfosRepository _userInfosRepository = userInfosRepository;

        public async Task<UserInfo> GetUserInfo(int userId) =>
            await _userInfosRepository.Get(userId);

        public async Task<int> UpdateUserInfo(int userId, string? firstName, string? middleName, string? lastName) =>
            await _userInfosRepository.Update(userId, firstName, middleName, lastName);

        public async Task<int> DeleteUserInfo(int userId) =>
            await _userInfosRepository.Delete(userId);
    }
}
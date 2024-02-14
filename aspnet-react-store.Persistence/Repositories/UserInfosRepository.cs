using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Persistence.Repositories
{
    public class UserInfosRepository(StoreDbContext context, IMapper mapper) : IUserInfosRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<UserInfo> Get(int userId)
        {
            var userInfo = await _context.UserInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(ui => ui.UserId == userId)
                    ?? throw new Exception("User info not found.");

            return _mapper.Map<UserInfo>(userInfo)!;
        }

        public async Task<int> Update(int userId, string? firstName, string? middleName, string? lastName)
        {
            if (firstName is null && middleName is null && lastName is null)
                return 0;

            await _context.UserInfos
                .Where(ui => ui.UserId == userId)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(ui => ui.FirstName, ui => firstName ?? ui.FirstName)
                    .SetProperty(ui => ui.MiddleName, ui => middleName ?? ui.MiddleName)
                    .SetProperty(ui => ui.LastName, ui => lastName ?? ui.LastName));

            return userId;
        }

        public async Task<int> Delete(int userId)
        {
            await _context.UserInfos
                .Where(ui => ui.UserId == userId)
                .ExecuteDeleteAsync();

            return userId;
        }
    }
}
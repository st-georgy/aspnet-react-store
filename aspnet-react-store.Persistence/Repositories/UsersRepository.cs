using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Repositories
{
    public class UsersRepository(StoreDbContext context, IMapper mapper) : IUsersRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<User> GetById(int id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id)
                    ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity)!;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email)
                    ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity)!;
        }

        public async Task<User> GetByUsername(string username)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == username)
                    ?? throw new Exception("User not found");

            return _mapper.Map<User>(userEntity)!;
        }

        public async Task Add(User user)
        {
            var userEntityEmail = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userEntityEmail is not null)
                throw new Exception("Email is busy");

            var userEntityUsername = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (userEntityUsername is not null)
                throw new Exception("Username is busy");

            var userEntity = _mapper.Map<UserEntity>(user)!;

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(int id, string? username, string? email)
        {
            if (username is null && email is null)
                return 0;

            if (username?.Trim().Length == 0 || email?.Trim().Length == 0)
                throw new Exception("Username and email can not be white space.");

            var userEntityEmail = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Id != id);

            if (userEntityEmail is not null)
                throw new Exception("Email is busy");

            var userEntityUsername = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == username && u.Id != id);

            if (userEntityUsername is not null)
                throw new Exception("Username is busy");

            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(up => up
                    .SetProperty(u => u.UserName, u => username ?? u.UserName)
                    .SetProperty(u => u.Email, u => email ?? u.Email));

            return id;
        }

        public async Task<int> GetNextId() =>
            await _context.GetNextSequenceValue("User_Id_seq");
    }
}

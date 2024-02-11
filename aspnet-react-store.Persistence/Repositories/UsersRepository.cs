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

        public async Task<int> GetNextId() =>
            await _context.GetNextSequenceValue("User_Id_seq");
    }
}

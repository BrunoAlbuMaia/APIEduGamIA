using Domain.Entitys;
using Domain.Interfaces;
using Infrastructure.Data.Repositories.Interfaces;
using Shared.Requests;

namespace Service
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUserAsync(UsersRequests users)
        {
            UsersEntity user = new UsersEntity
            {
                firstName = users.firstName,
                lastName = users.lastName,
                UserCompl = new UsersComplEntity
                {
                    email = users.email,
                    username = users.username,
                    password_hash = BCrypt.Net.BCrypt.HashPassword(users.passwordHash),
                    preferences = users.preferences,
                    role = users.role
                },

            };
            
            return await _userRepository.RegisterUser(user);
            
        }

        
    }
}

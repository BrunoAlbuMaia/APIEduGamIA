using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys;
using Shared.Dtos.Responses;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UsersEntity> UserByUsername(string username);
        Task<bool> RegisterUser(UsersEntity users);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys;

namespace Infrastructure.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> UserByUsername(string username);
    }
}

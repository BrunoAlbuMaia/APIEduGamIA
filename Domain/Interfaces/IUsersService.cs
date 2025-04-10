using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys;
using Shared.Dtos.Responses;
using Shared.Requests;

namespace Domain.Interfaces
{
    public interface IUsersService
    {

        Task<bool> CreateUserAsync(UsersRequests users);


    }
}

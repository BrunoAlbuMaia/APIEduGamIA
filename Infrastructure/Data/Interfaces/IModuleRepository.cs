using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;
using Shared.Response;

namespace Infrastructure.Data.Interfaces
{
    public interface IModuleRepository
    {
        Task<ModulePostResponse> Create(ModuleEntity module);

        Task<PaginatedResponse<ModuleEntity>> GetAll(int page, int size, string? name = null);

    }
}

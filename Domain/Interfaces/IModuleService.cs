using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Domain.Interfaces
{
    public interface IModuleService
    {
        Task<PaginatedResponse<ModuleEntity>> GetAll(int page, int size, string? name = null);
        Task<ModulePostResponse> Create(ModuleRequests disciplina);
    }
}

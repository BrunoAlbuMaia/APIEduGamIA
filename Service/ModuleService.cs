using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Interfaces;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Service
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepo;

        public ModuleService(IModuleRepository moduleRepo)
        {
            _moduleRepo = moduleRepo;
        }
        public async Task<ModulePostResponse> Create(ModuleRequests module)
        {
            var moduleEntity = new ModuleEntity
            {
                name = module.name,
                description = module.description,
                user_create = module.Username,
                user_update = module.Username,

            };
            return await _moduleRepo.Create(moduleEntity);
        }

        public async Task<PaginatedResponse<ModuleEntity>> GetAll(int page, int size, string? name = null)
        {
            return await _moduleRepo.GetAll(page, size, name);
        }
    }
}

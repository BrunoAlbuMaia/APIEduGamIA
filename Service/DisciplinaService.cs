using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Connections;
using Shared.Dtos.Responses;
using Shared.Requests;
using Shared.Response;

namespace Service
{
    public class DisciplinaService : IDisciplineService
    {
        private readonly IDisciplineRepository _disciplineRepo;

        public DisciplinaService(IDisciplineRepository disciplineRepo)
        {
            _disciplineRepo = disciplineRepo;
        }
        public async Task<DisciplinePostResponse> Create(DisciplineRequests disciplina)
        {
            var disciplinaEntity = new DisciplineEntity
            {
                name = disciplina.name,
                description = disciplina.description,
                user_create = disciplina.Username,
                user_update = disciplina.Username,
                
            };
            return await _disciplineRepo.Create(disciplinaEntity);
        }
        public async Task<PaginatedResponse<DisciplineEntity>> GetAll(int page, int size, string? name = null)
        {
            return await _disciplineRepo.GetAll(page, size, name);
        }
        public Task<DisciplineEntity> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Update(DisciplineEntity disciplina)
        {
            throw new NotImplementedException();
        }
    }
}

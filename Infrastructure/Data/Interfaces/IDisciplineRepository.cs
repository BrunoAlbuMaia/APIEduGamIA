using Domain.Entities;
using Shared.Dtos.Responses;
using Shared.Response;

namespace Infrastructure.Data.Interfaces
{
    public interface IDisciplineRepository
    {
        Task<DisciplinePostResponse> Create(DisciplineEntity disciplina);
        Task<PaginatedResponse<DisciplineEntity>> GetAll(int page, int size, string? name = null);
        Task<DisciplineEntity> GetById(int id);
        Task<bool> Update(DisciplineEntity disciplina);
    }
}

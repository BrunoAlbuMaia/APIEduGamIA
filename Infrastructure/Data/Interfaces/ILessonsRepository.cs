using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;

namespace Infrastructure.Data.Interfaces
{
    public interface ILessonsRepository
    {
        Task<PaginatedResponse<LessonsEntity>> GetAllByModuleId(int moduleId, int page, int size,string title);

        Task<bool> PostLessons(LessonsEntity entity);


    }
}

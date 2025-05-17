using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;

namespace Domain.Interfaces
{
    public interface ILessonsService
    {
        Task<PaginatedResponse<LessonsEntity>> GetAll(int moduleId, int page, int size, string title);
    }
}

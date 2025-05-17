using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Interfaces;
using Shared.Dtos.Responses;

namespace Service
{
    public class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository _lessons;
        public LessonsService(ILessonsRepository lessons )
        {
            _lessons = lessons;
        }
        public async Task<PaginatedResponse<LessonsEntity>> GetAll(int moduleId, int page, int size, string title)
        {
            return await _lessons.GetAllByModuleId(moduleId, page, size, title);
        }
    }
}

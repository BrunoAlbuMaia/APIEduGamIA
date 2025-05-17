using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;

namespace Infrastructure.Data.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<PaginatedResponse<QuestionsEntity>> GetAll(int lessonsId,int page, int size);
    }
}

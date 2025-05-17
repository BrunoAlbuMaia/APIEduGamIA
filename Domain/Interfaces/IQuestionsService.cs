using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.Dtos.Responses;

namespace Domain.Interfaces
{
    public interface IQuestionsService
    {
        Task<PaginatedResponse<QuestionsEntity>> GetQuestionsAsync(int lessonsId,int page, int size);
    }
}

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
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questions;
        public QuestionsService(IQuestionsRepository questions)
        {
            _questions = questions;
        }
        public async Task<PaginatedResponse<QuestionsEntity>> GetQuestionsAsync(int lessonsId, int page, int size)
        {
            return await _questions.GetAll(lessonsId, page, size);
        }
    }
}

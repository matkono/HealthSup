﻿using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetInitialByDisease
        (
            int diseaseId
        );
    }
}

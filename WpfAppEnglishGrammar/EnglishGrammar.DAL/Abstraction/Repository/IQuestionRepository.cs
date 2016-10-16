
using System.Collections.Generic;
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IQuestionRepository
    {
        List<Question> GetAllUsersQuestion();
    }
}

using System.Collections.Generic;
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IMarkAnswerRepository
    {
        void InsertMarkAnswer(MarkAnswers myMark, int MarkId);
        List<MarkAnswers> GetAllUsersMarkAnswers();
        List<MarkAnswers> GetMarksForCurrentUser(string login);
    }
}

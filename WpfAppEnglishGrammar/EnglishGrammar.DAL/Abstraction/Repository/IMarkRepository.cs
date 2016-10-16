
using System.Collections.Generic;
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IMarkRepository
    {
        List<UserMarks> GetAllUsersScores();
        void InsertMark(Mark myMark);
        List<Mark> GetAllMarks();
    }
}

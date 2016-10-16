using System.Collections.Generic;
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IUserMarkRepository
    {
        List<UserMarks> GetAllLogins();
    }
}

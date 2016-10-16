
using System.Collections.Generic;
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface ITestRepository
    {
        List<Test> GetAllTestForUserWithMarks(string login);
        List<Test> GetAllTests();
    }
}


using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class QuestionParser
    {
        public static Question GetQuestion(SqlDataReader reader)
        {
            var question = new Question();
            question.Id = (int)reader["Id"];
            question.QuestionText = (string)reader["Question"];
            question.TestId = (int)reader["TestId"];
            question.Theme = new QuestionTheme((int)reader["ThemeId"], (string)reader["Theme"]);

            return question;
        }
    }
}


using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class MarkAnswerParser
    {
        public static MarkAnswers GetMarkAnswer(SqlDataReader reader)
        {
            var markAnswer = new MarkAnswers();

            markAnswer.MarkId = (int)reader["MarkId"];
            markAnswer.AnswerId = (int)reader["AnswerId"];

            return markAnswer;
        }
    }
}

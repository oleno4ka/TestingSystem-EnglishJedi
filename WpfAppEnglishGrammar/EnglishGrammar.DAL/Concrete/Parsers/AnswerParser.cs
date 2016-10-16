
using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class AnswerParser
    {
        public static Answer GetAnswer(SqlDataReader reader)
        {
            var answer = new Answer();

            answer.Id = (int)reader["Id"];
            answer.QuestionId = (int)reader["QuestionId"];
            answer.QuestionAnswer = (string)reader["Answer"];
            answer.IsRight = (bool)reader["IsRight"];

            return answer;
        }
    }
}

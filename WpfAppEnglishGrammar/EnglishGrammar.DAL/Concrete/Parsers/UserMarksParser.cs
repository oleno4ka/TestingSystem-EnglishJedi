
using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class UserMarksParser
    {
        public static UserMarks GetUserLogin(SqlDataReader reader)
        {
            var user = new UserMarks();

            user.Login = (string)reader["UserLogin"];

            return user;
        }
    }
}

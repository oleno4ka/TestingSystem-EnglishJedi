
using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class MarkParser
    {
        public static Mark GetMark(SqlDataReader reader)
        {
            var mark = new Mark();

            mark.Id = (int)reader["MarkId"];
            mark.TestId = (int)reader["TestId"];

            return mark;
        }

        public static Mark GetMarkWithUserLogin(SqlDataReader reader)
        {
            var mark = new Mark();

            mark.Id = (int)reader["Id"];
            mark.TestId = (int)reader["TestId"];
            mark.User = new UserMarks { Login = (string)reader["UserLogin"] };        

            return mark;
        }
        public static Mark GetMarkIdFromInserted(SqlDataReader reader)
        {
            var mark = new Mark();

            mark.Id = (int)reader["Id"];
           
            return mark;
        }

    }
}

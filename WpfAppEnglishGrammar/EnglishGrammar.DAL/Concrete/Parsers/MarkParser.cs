
using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class MarkParser
    {
      
        //Review - Oleg: Duplication of code! You do not need so many methods to
        //create a parser, you only need to create one parser method to create models instances.
        
        
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

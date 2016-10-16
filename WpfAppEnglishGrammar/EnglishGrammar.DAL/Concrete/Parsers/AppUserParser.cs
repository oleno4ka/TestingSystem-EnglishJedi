
using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class AppUserParser
    {
        public static AppUser SetNewUser(SqlDataReader reader)
        {
            var user = new AppUser();
            
            user.Id = (int)reader["Id"];
            user.FirstName = (string)reader["FirstName"];
            user.LastName = (string)reader["LastName"];
            user.Login = (string)reader["UserLogin"];

            return user;
        }
        public static AppUser GetUserByLogin(SqlDataReader reader)
        {
            var user = new AppUser();

            user.Id = (int)reader["Id"];
            user.FirstName = (string)reader["FirstName"];
            user.LastName = (string)reader["LastName"];
            user.Login = (string)reader["UserLogin"];

            return user;
        }
        public static AppUser GetUserLogin(SqlDataReader reader)
        {
            var user = new AppUser();
          
            user.Login = (string)reader["UserLogin"];

            return user;
        }
    }
}

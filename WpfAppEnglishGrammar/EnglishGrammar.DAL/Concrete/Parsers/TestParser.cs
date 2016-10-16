using System;

using EnglishGrammar.Entities;
using System.Data.SqlClient;

namespace EnglishGrammar.DAL.Concrete.Parsers
{
    public static class TestParser
    {
        public static Test GetTest(SqlDataReader reader)
        {
            var tempTest = new Test();

            tempTest.Id = (int)reader["Id"];
            tempTest.Name = (string)reader["Name"];
            tempTest.Description = (string)reader["TestDescription"];
            tempTest.Duration = (TimeSpan)reader["Duration"];
            tempTest.PassValue = (int)reader["PassValue"];
            tempTest.TestLevel = new TestLevel((int)reader["LevelId"], (string)reader["TestLevel"]);

            return tempTest;
        }
    }
}

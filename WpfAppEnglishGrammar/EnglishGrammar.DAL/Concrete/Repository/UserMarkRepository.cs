using System.Collections.Generic;
using EnglishGrammar.Entities;
using EnglishGrammar.DAL.Concrete.Parsers;
using EnglishGrammar.DAL.Abstraction.Repository;
using System;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class UserMarkRepository : GenericRepository<UserMarks>, IUserMarkRepository
    {
        #region Stored Procedures
        private const string spGetAllUsersLogins = "spGetAllUsersLogins";
        #endregion
        #region Private Fields

        private readonly string _connectionString;

        #endregion
        #region Constructor
        public UserMarkRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region IUserMarkRepository

        public List<UserMarks> GetAllLogins()
        {
            try
            { 
               List<UserMarks> userList = new List<UserMarks>();
               var user = base.ExecuteReader(spGetAllUsersLogins, UserMarksParser.GetUserLogin);
               userList.AddRange(user);

               return userList;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception cathed in GetAllTestForUserWithMarks: " + e.Message);
                return null;
            }
        }

        #endregion
    }
}

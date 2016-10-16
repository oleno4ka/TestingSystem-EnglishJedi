using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using EnglishGrammar.DAL.Concrete.Parsers;
using System.Linq;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
       
        #region StoredProcedures

        private const string spGetUserByLoginQuery = "spGetUserByLogin";
        private const string spGetAllUsersLogins = "spGetAllUsersLogins";
        private const string spSetNewUser = "spSetNewUser";

        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructors

        public AppUserRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region IAppUserRepository

        public bool IsUserAlowed(string login)
        {
            try
            {
                List<AppUser> userList = new List<AppUser>();

                var user = base.ExecuteReader(spGetAllUsersLogins, AppUserParser.GetUserLogin);
                userList.AddRange(user);
                foreach (AppUser item in userList)
                {
                    if (item.Login == login)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
               MessageBox.Show("Exception cathed in IsUserAlowed: " + ex.Message);
                return false;
            }
        }
        public void SetNewUser(AppUser _newUser)
        {
            var parameters = new[]
              {
                   new SqlParameter("@Login",_newUser.Login),
                   new SqlParameter("@Password", Servises.UserService.MD5Hash(_newUser.Password)),
                   new SqlParameter("@FirstName", _newUser.FirstName),
                   new SqlParameter("@LastName", _newUser.LastName)
               };
            try
            {
                var user = base.ExecuteReader(spSetNewUser, AppUserParser.GetUserByLogin,parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in SetNewUser: " + ex.Message);              
            }
        }
        public AppUser GetUserByLogin(string login, string password)
        {
            var parameters = new[]
            {
                   new SqlParameter("@Login",login),
                   new SqlParameter("@Password", Servises.UserService.MD5Hash(password)),
               };
            try
            {
                var user = base.ExecuteReader(spGetUserByLoginQuery, AppUserParser.GetUserByLogin, parameters);
            AppUser us = new AppUser();
            us = (AppUser)user.First();
            return us;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetUserByLogin: " + ex.Message);
                return null;
            }
        }

        #endregion
    }
}

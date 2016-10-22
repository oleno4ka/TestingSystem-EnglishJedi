using System.Collections.Generic;
using EnglishGrammar.DAL.Concrete.Parsers;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;
using System.Data.SqlClient;
using System;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class MarkAnswerRepository : GenericRepository<MarkAnswers>, IMarkAnswerRepository
    {
        #region Stored Procedures

        private const string spSetMarkAnswer = "spSetMarkAnswer";
        private const string spGetAllMarkAnswers = "spGetAllMarkAnswers";
        private const string spGetMarksForCurrentUser = "spGetMarksForCurrentUser";

        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructor
        public MarkAnswerRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region IMarkAnswerRepository

        public void InsertMarkAnswer(MarkAnswers myMark,int MarkId)
        {
        // Review - Oleg Shandra: Format your code
            try
            {
                var parameters2 = new[]
              {
                   new SqlParameter("@MarkId", MarkId),
                   new SqlParameter("@AnswerId", myMark.AnswerId)
               };
            var user2 = base.ExecuteReader(spSetMarkAnswer, MarkAnswerParser.GetMarkAnswer, parameters2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in InsertMarkAnswer: " + ex.Message);
            }
        }

        public List<MarkAnswers> GetAllUsersMarkAnswers()
        {
            try
            {
                List<MarkAnswers> userList = new List<MarkAnswers>();
            var user = base.ExecuteReader(spGetAllMarkAnswers, MarkAnswerParser.GetMarkAnswer);
            userList.AddRange(user);

            return userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetAllUsersMarkAnswers: " + ex.Message);
                return null;
            }
        }

        public List<MarkAnswers> GetMarksForCurrentUser(string login)
        {
            try
            {
                var parameters = new[]
             {
                   new SqlParameter("@Login", login)                   
               };
            List<MarkAnswers> userList = new List<MarkAnswers>();
            var user = base.ExecuteReader(spGetMarksForCurrentUser, MarkAnswerParser.GetMarkAnswer,parameters);
            userList.AddRange(user);

            return userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetMarksForCurrentUser: " + ex.Message);
                return null;
            }
        }

        #endregion
    }
}

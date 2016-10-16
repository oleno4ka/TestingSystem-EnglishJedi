
using System.Collections.Generic;
using EnglishGrammar.DAL.Concrete.Parsers;
using EnglishGrammar.Entities;
using EnglishGrammar.DAL.Abstraction.Repository;
using System;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        #region StoredProcedures      

        private const string spGetAllUsersQuestions = "spGetAllUsersQuestions";

        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructors

        public QuestionRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region IQuestionRepository

        public List<Question> GetAllUsersQuestion()
        {
            try
            {
                List<Question> userList = new List<Question>();
                var user = base.ExecuteReader(spGetAllUsersQuestions, QuestionParser.GetQuestion);
                userList.AddRange(user);

                return userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetAllUsersScores: " + ex.Message);
                return null;
            }
        }

        #endregion
    }
}

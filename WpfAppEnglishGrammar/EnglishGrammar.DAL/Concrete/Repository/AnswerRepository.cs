using System;
using System.Collections.Generic;
using EnglishGrammar.DAL.Concrete.Parsers;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;


namespace EnglishGrammar.DAL.Concrete.Repository
{
    class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        #region StoredProcedures      

        private const string spGetAllUsersAnswers = "spGetAllUsersAnswers";
      
        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructors

        public AnswerRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region IAnswerRepository

        public List<Answer> GetAllUsersAnswers()
        {
            try
            {
                List<Answer> userList = new List<Answer>();
                var user = base.ExecuteReader(spGetAllUsersAnswers, AnswerParser.GetAnswer);
                userList.AddRange(user);

                return userList;
            }           
            
            // Review - Olef Shandra: Format your code
            catch(Exception ex)
            {
                throw new Exception("Exception cathed in GetAllUsersAnswers: " + ex.Message, ex);
    }

}

        #endregion
    }
}


using System.Collections.Generic;
using System.Linq;
using EnglishGrammar.DAL.Concrete.Parsers;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class MarkRepository : GenericRepository<Mark>, IMarkRepository
    {
        #region StoredProcedures      

        private const string spSetMarks = "spSetMarks";
        private const string spSetMarkAnswer = "spSetMarkAnswer";
        private const string spGetAllUsersMarks = "spGetAllUsersMarks";

        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructors

        public MarkRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region IMarkRepository

        public List<Mark> GetAllMarks()
        {
            try
            {
                List<Mark> userList = new List<Mark>();
            var user = base.ExecuteReader(spGetAllUsersMarks, MarkParser.GetMarkWithUserLogin);
            userList.AddRange(user);

            return userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetAllMarks: " + ex.Message);
                return null;
            }
        }

        public void InsertMark(Mark myMark)
        {
            try
            {
                var parameters = new[]
             {
                   new SqlParameter("@UserId", myMark.UserId),
                   new SqlParameter("@TestId", myMark.TestId),
                   new SqlParameter("@Value", myMark.Value)
               };
            var user = base.ExecuteReader(spSetMarks, MarkParser.GetMarkIdFromInserted, parameters);

            int insertedMarkId = user.First().Id;
            MarkAnswerRepository _markAnswerRepository = new MarkAnswerRepository(_connectionString);
            foreach (MarkAnswers item in myMark.MarkAnswer)
            {
                _markAnswerRepository.InsertMarkAnswer(item, insertedMarkId);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in InsertMark: " + ex.Message);              
            }
        }

        public List<UserMarks> GetAllUsersScores()
        {
            try
            {
                List<UserMarks> userList;
                List<Mark> marksList;
                List<Answer> listAnsw;
                List<Test> listTest;
                List<Question> listQuestion;
                List<MarkAnswers> markAnsw;

                UserMarkRepository _userMarkRepository = new UserMarkRepository(_connectionString);
                userList = _userMarkRepository.GetAllLogins();

                AnswerRepository _answerRepository = new AnswerRepository(_connectionString);
                listAnsw = _answerRepository.GetAllUsersAnswers();

                QuestionRepository _questionRepository = new QuestionRepository(_connectionString);
                listQuestion = _questionRepository.GetAllUsersQuestion();
                foreach (Question item in listQuestion)
                {
                    item.Answers = new List<Answer>(listAnsw.Where(f => f.QuestionId == item.Id));
                }

                MarkAnswerRepository _markAnswerRepository = new MarkAnswerRepository(_connectionString);
                markAnsw = _markAnswerRepository.GetAllUsersMarkAnswers();
                foreach (MarkAnswers item in markAnsw)
                {
                    item.Answer = listAnsw.Where(f => f.Id == item.AnswerId).First();
                }

                TestRepository _testRepository = new TestRepository(_connectionString);
                listTest = new List<Test>();
                listTest = _testRepository.GetAllTests();

                marksList = new List<Mark>();

                var user = base.ExecuteReader(spGetAllUsersMarks, MarkParser.GetMarkWithUserLogin);
                marksList.AddRange(user);

                foreach (Mark item in marksList)
                {
                    item.MarkAnswer = new List<MarkAnswers>(markAnsw.Where(f => f.MarkId == item.Id));
                    item.Test = listTest.Where(f => f.Id == item.TestId).FirstOrDefault();
                }

                foreach (UserMarks item in userList)
                {
                    item.Marks = new List<Mark>();
                    item.Marks.AddRange(marksList.Where(m => m.User.Login == item.Login));
                }
                foreach (Answer item in listAnsw)
                {
                    item.Question = listQuestion.Where(m => m.Id == item.QuestionId).FirstOrDefault();
                }
                foreach (Question item in listQuestion)
                {
                    item.Test = listTest.Where(m => m.Id == item.TestId).FirstOrDefault();

                }
                foreach (Test item in listTest)
                {
                    item.Marks = new List<Mark>();
                    item.Marks.AddRange(marksList.Where(m => m.TestId == item.Id));
                    item.Questions = new List<Question>();
                    item.Questions.AddRange(listQuestion.Where(m => m.TestId == item.Id));
                }
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

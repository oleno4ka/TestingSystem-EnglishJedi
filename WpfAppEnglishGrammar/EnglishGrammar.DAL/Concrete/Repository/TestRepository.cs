using System;
using System.Collections.Generic;
using System.Linq;
using EnglishGrammar.DAL.Abstraction.Repository;
using EnglishGrammar.Entities;
using System.Data;
using EnglishGrammar.DAL.Concrete.Parsers;
using System.Windows;

namespace EnglishGrammar.DAL.Concrete.Repository
{
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        #region StoredProcedures

        private const string spGetAllTests = "spGetAllTests";
       
        #endregion

        #region Private Fields

        private readonly string _connectionString;

        #endregion

        #region Constructors

        public TestRepository(string connectionString)
            : base(connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region ITestRepository

        public List<Test> GetAllTests()
        {
            try
            { 
               List<Test> userList = new List<Test>();
               var user = base.ExecuteReader(spGetAllTests, TestParser.GetTest);
               userList.AddRange(user);

               return userList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception cathed in GetAllUsersScores: " + ex.Message);
                return null;
            }
            
            // Review - Oleg Shandra: Format your code
            
}

        public List<Test> GetAllTestForUserWithMarks(string login)
        {
            List<Test> test = new List<Test>();
            try
            {
                List<Mark> marksList = new List<Mark>();
                List<Question> questionList = new List<Question>();

                List<Answer> answerList = new List<Answer>();
                List<MarkAnswers> markAnsw = new List<MarkAnswers>();

                AnswerRepository _answerRepository = new AnswerRepository(_connectionString);
                answerList = _answerRepository.GetAllUsersAnswers();

                MarkAnswerRepository _markAnswerRepository = new MarkAnswerRepository(_connectionString);
                markAnsw = _markAnswerRepository.GetMarksForCurrentUser(login);
                foreach (MarkAnswers item in markAnsw)
                {
                    item.Answer = answerList.Where(f => f.Id == item.AnswerId).First();
                }

                MarkRepository _markRepository = new MarkRepository(_connectionString);
                marksList = _markRepository.GetAllMarks();
                foreach (Mark item in marksList)
                {
                    item.MarkAnswer = new List<MarkAnswers>(markAnsw.Where(f => f.MarkId == item.Id));                   
                }

                QuestionRepository _questionRepository = new QuestionRepository(_connectionString);
                questionList = _questionRepository.GetAllUsersQuestion();
                foreach (Question item in questionList)
                {
                    item.Answers = new List<Answer>(answerList.Where(f => f.QuestionId == item.Id));
                    foreach (Answer itemA in answerList.Where(m => m.QuestionId == item.Id))
                    {
                        itemA.Question = item;
                    }
                }
                

                var user = base.ExecuteReader(spGetAllTests, TestParser.GetTest);
                test.AddRange(user);
                foreach (Test tempTest in test)
                {
                    tempTest.Marks = new List<Mark>();
                    tempTest.Questions = new List<Question>();
                    foreach (Mark item in marksList.Where(m => m.TestId == tempTest.Id))
                    {
                        tempTest.Marks.Add(item);
                        tempTest.Marks.Last<Mark>().Test = tempTest;                        
                    }
                    foreach (Question item in questionList.Where(m => m.TestId == tempTest.Id))
                    {
                        tempTest.Questions.Add(item);
                        tempTest.Questions.Last<Question>().Test = tempTest;
                    }
                }
                return test;
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


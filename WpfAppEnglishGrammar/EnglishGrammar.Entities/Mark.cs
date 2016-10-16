
using System.Collections.Generic;
using System.Linq;

namespace EnglishGrammar.Entities
{
    public class Mark : IBaseEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }

        public int Value { get { return (MarkAnswer != null && MarkAnswer.Count > 0) ? MarkAnswer.Where(c => c.Answer.IsRight).Count() :0; } }
        public int PercentValue { get { return (Test != null && Test.QuestionsCount > 0) ? Value * 100 / Test.QuestionsCount :0 ; } } 
        public Test Test { get; set; }
        public UserMarks User { get; set; }
        public List<MarkAnswers> MarkAnswer { get; set; }

        public List<ThemeScore> ThemsList
        {
            get
            {
                List<ThemeScore> returnList = new List<ThemeScore>() { };

                foreach (QuestionTheme qt in Test.Questions.GroupBy(c => c.Theme.Id).Select(group => group.First().Theme).ToList())
                {
                    returnList.Add(new ThemeScore() { Theme = qt });
                }
                foreach (ThemeScore ts in returnList)
                {
                    ts.Score = MarkAnswer.Where(f => f.Answer.Question.Theme.Id == ts.Theme.Id).Where(c => c.Answer.IsRight).Count()
                    * 100 /
                    Test.Questions.Where(f => f.Theme.Id == ts.Theme.Id).Count();
                }
                return returnList;
            }
        }
    }
}


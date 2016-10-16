
using System.Collections.Generic;

namespace EnglishGrammar.Entities
{
    public class Question : IBaseEntity
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public int TestId { get; set; }       

        public QuestionTheme Theme { get; set; }

        public List<Answer> Answers { get; set; }

        public Test Test { get; set; }

        
    }
}

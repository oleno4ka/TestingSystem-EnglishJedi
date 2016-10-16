using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishGrammar.Entities
{
    public class Test : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }
        
        public TestLevel TestLevel { get; set; }

        public List<Question> Questions { get; set; }
        public List<Mark> Marks { get; set; }

        public int PassValue { get; set; }

        public int QuestionsCount
        {
            get
            {
                return Questions.Count;
            }
        }
        public int MaxMark
        {
            get
            {
                return Marks.Max(m => m.Value);
            }
        }
        public int MaxPercent
        { 
            get
            {
                return (int)Marks.Max(m => m.PercentValue) ;
            }
        }
        public int Attempts
        {
            get
            {
                return Marks.Count;
            }
        }
    }
}

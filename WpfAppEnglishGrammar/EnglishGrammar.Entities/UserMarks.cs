using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishGrammar.Entities
{
    public class UserMarks
    {
        public string Login { get; set; }

        //Review - Oleg Shandra: It is difficult to read so many lambda expressions in get accessors. 
         //In my opinion, it was better to make a single stored procedures for finding these values.
         //The same issue in Mark.cs.
        public int Score { get { return (Marks != null && Marks.Count > 0) ? Marks.GroupBy(v => v.TestId).Select(group => group.Max(f => f.Value)*group.Select(v => v.Test.TestLevel.ScoreCoeficient).First()).Sum(): 0 ; } } 
        public double AverageScore { get { return (Marks != null && Marks.Count > 0) ?  (int)Marks.GroupBy(v => v.TestId).Select(group => group.Max(f => f.Value)).Average() :0 ; } }
        public double PercentScore { get { return (Marks != null && Marks.Count > 0) ?  (int)Marks.GroupBy(v => v.TestId).Select(group => group.Average(f => f.PercentValue)).Average() :0 ; } }
        public List<Mark> Marks { get; set; }

        public List<ThemeScore> ThemasScore
        {
            get
            {
                return Marks.GroupBy(a => a.Test.Id).Select(b => b.SelectMany(c => c.ThemsList).ToList()).ToList()
                    .Select(d => d.GroupBy(e => e.Theme)).SelectMany(f => f.Select(g => g.Where(h => h.Score == g.Max(i => i.Score)).FirstOrDefault())).ToList()
                    .GroupBy(k => k.Theme.Id).Select(l => new ThemeScore { Theme = l.First().Theme, Score = l.Average(m => m.Score) }).ToList();
            }

        }
    }
    
}

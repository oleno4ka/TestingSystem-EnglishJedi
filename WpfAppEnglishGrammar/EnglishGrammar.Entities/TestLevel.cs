
namespace EnglishGrammar.Entities
{
    public class TestLevel : IBaseEntity
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public int ScoreCoeficient { get { return Id; }  }

        public TestLevel(int tLevelId, string tLevel)
        {
            Id = tLevelId;
            Level = tLevel;
        }
    }
}

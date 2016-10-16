
namespace EnglishGrammar.Entities
{
    public class QuestionTheme : IBaseEntity
    {
        public int Id { get; set; }

        public string Theme{ get; set; }

        public QuestionTheme(int _id, string _theme)
        {
            Id = _id;
            Theme = _theme;
        }
    }
}

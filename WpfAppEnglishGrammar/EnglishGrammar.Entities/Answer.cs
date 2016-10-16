

namespace EnglishGrammar.Entities
{
    public class Answer : IBaseEntity
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string QuestionAnswer { get; set; }

        public bool IsRight { get; set; }

        public Question Question { get; set; }

    }
}

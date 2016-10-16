

namespace EnglishGrammar.Entities
{
    public class MarkAnswers : IBaseEntity
    {
        public int Id { get; set; }

        public int MarkId { get; set; }

        public int AnswerId { get; set; }

        public Answer Answer { get; set; }
    }

}

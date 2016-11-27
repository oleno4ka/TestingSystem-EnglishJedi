
namespace EnglishGrammar.Entities
{
    // Review TK: It is strange to create Interface IBaseEntity with just Id field.
    public interface IBaseEntity
    {       
       int Id { get; set; }       
    }
}

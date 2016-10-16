
using EnglishGrammar.Entities;

namespace EnglishGrammar.DAL.Abstraction.Repository
{
    public interface IAppUserRepository
    {
        AppUser GetUserByLogin(string login, string password);
        bool IsUserAlowed(string login);
        void SetNewUser(AppUser _newUser);
    }
}

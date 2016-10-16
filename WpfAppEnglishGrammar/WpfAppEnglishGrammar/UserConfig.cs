using System;
using EnglishGrammar.Entities;

namespace WpfAppEnglishGrammar
{
    public class UserConfig
    {
        private static AppUser user;

        public static void Initialize(AppUser _user)
        {
            if (user != null)
            {
                throw new InvalidOperationException("User is already chosen");
            }
            user = _user;
        }

        public static int Id
        {
            get
            {
                return user.Id;
            }
        }

        public static string FirstName
        {
            get
            {
                return user.FirstName;
            }
        }

        public static string LastName
        {
            get
            {
                return user.LastName;
            }
        }

        public static string Login
        {
            get
            {
                return user.Login;
            }
        }

    }
}

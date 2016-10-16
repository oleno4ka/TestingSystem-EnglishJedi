using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishGrammar.Entities
{
    public class AppUser : IBaseEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Login adress is required")]       
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 1)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public List<Mark> Marks { get; set; }
    }
}

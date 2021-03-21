using System.ComponentModel.DataAnnotations;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class UserInsertViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and password confirmation must be the same.")]
        public string CheckPassword { get; set; }
    }
}
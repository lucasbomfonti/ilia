using Ilia.CrossCutting.Interop.Base;
using System.ComponentModel.DataAnnotations;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class CustomerUpdateViewModel : BaseUpdateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

    }
}
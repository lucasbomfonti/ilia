using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class CustomerInsertViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public List<CustomerPhoneContactInsertViewModel> PhoneContact { get; set; }
        public List<CustomerAddressInsertViewModel> Address { get; set; }
    }
}
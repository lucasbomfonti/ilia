using System;
using System.ComponentModel.DataAnnotations;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class PhoneContactInsertViewModel : CustomerPhoneContactInsertViewModel
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
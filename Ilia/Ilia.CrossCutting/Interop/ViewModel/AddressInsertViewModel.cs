using System;
using System.ComponentModel.DataAnnotations;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class AddressInsertViewModel : CustomerAddressInsertViewModel
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
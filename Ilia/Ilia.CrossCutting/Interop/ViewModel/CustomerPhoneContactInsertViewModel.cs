using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ilia.Domain.Enum;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class CustomerPhoneContactInsertViewModel
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public EnumPhoneType PhoneType { get; set; }
    }
}

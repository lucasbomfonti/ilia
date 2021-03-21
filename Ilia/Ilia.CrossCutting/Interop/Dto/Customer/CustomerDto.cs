using System;
using System.Collections.Generic;

namespace Ilia.CrossCutting.Interop.Dto.Customer
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<PhoneContactDto> PhoneContact { get; set; }
        public virtual ICollection<AddressDto> Address { get; set; }
        public int Version { get; set; }

    }
}
using Ilia.Domain.Enum;
using System;

namespace Ilia.CrossCutting.Interop.Dto.Customer
{
    public class PhoneContactDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public EnumPhoneType PhoneType { get; set; }
        public int Version { get; set; }

    }
}
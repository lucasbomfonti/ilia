using Ilia.Domain.Base;
using Ilia.Domain.Enum;
using System;

namespace Ilia.Domain
{
    public class PhoneContact : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Number { get; set; }
        public EnumPhoneType PhoneType { get; set; }
    }
}
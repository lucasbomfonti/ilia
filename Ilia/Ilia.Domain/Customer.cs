using Ilia.Domain.Base;
using System.Collections.Generic;

namespace Ilia.Domain
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<PhoneContact> PhoneContact { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
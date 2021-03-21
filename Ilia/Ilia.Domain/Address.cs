using System;
using System.Collections.Generic;
using System.Text;
using Ilia.Domain.Base;

namespace Ilia.Domain
{
    public class Address : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}

using System;

namespace Ilia.CrossCutting.Interop.Dto.Customer
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Version { get; set; }

    }
}
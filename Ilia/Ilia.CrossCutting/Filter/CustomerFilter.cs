using Ilia.CrossCutting.Filter.Base;

namespace Ilia.CrossCutting.Filter
{
    public class CustomerFilter : BaseFilter
    {
     

        public int? Page { get; set; }
        public int? PerPage { get; set; }
        public string? Street { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public CustomerFilter()
        {
        }

        public CustomerFilter(string country, string state, string city, string zipCode, string street, int? perPage, int? page)
        {
            Country = country;
            State = state;
            City = city;
            ZipCode = zipCode;
            Street = street;
            PerPage = perPage;
            Page = page;
        }
    }
}
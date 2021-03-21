using Ilia.Data.Repository.Contracts.Base;
using Ilia.Domain;
using System;
using System.Threading.Tasks;

namespace Ilia.Data.Repository.Contracts
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Guid> AddNewPhoneContact(PhoneContact phoneContact);
        Task<Guid> AddNewAddress(Address address);
    }
}

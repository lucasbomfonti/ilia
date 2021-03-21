using Ilia.Data.Context;
using Ilia.Data.Repository.Base;
using Ilia.Data.Repository.Contracts;
using Ilia.Domain;
using System;
using System.Threading.Tasks;

namespace Ilia.Data.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public async Task<Guid> AddNewPhoneContact(PhoneContact phoneContact)
        {
            await Context.PhoneContact.AddAsync(phoneContact);
            await Context.SaveChangesAsync();
            return phoneContact.Id;
        }

        public async Task<Guid> AddNewAddress(Address address)
        {
            await Context.Address.AddAsync(address);
            await Context.SaveChangesAsync();
            return address.Id;
        }
    }
}
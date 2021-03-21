using Ilia.Business.Contracts.Base;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Domain;
using System;
using System.Threading.Tasks;
using Ilia.CrossCutting.Filter;

namespace Ilia.Business.Contracts
{
    public interface ICustomerService : IBaseService<Customer, CustomerFilter>
    {
        Task<Guid> CreatePhoneContact(PhoneContactInsertViewModel viewModel);
        Task<Guid> CreateAddress(AddressInsertViewModel viewModel);
    }
}
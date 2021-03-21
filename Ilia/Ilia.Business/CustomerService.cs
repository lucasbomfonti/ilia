using AutoMapper;
using Ilia.Business.Base;
using Ilia.Business.Contracts;
using Ilia.CrossCutting.Exceptions;
using Ilia.CrossCutting.Filter;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Repository.Contracts;
using Ilia.Data.RepositoryReadOnly.Contracts;
using Ilia.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ilia.Business
{
    public class CustomerService : BaseService<Customer, CustomerFilter>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerRepositoryReadOnly _customerRepositoryReadOnly;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepositoryReadOnly baseRepositoryReadOnly, ICustomerRepository baseRepository, IMapper mapper) : base(baseRepositoryReadOnly, baseRepository)
        {
            _customerRepository = baseRepository;
            _mapper = mapper;
            _customerRepositoryReadOnly = baseRepositoryReadOnly;
        }

        public override async Task<Guid> Create(Customer viewModel)
        {
            if (await _customerRepositoryReadOnly.GetCustomerByEmail(viewModel.Email) != null)
                throw new EntityValidationException("E-mail already registered");
            return await base.Create(viewModel);
        }

        public async Task<Guid> CreatePhoneContact(PhoneContactInsertViewModel viewModel)
        {
            var customer = await _customerRepositoryReadOnly.Find(viewModel.CustomerId);

            if (customer.PhoneContact.Any(w => w.Number.Equals(viewModel.Number)))
                throw new EntityValidationException("phone number already registered");

            return await _customerRepository.AddNewPhoneContact(_mapper.Map<PhoneContactInsertViewModel, PhoneContact>(viewModel));
        }

        public async Task<Guid> CreateAddress(AddressInsertViewModel viewModel)
        {
            return await _customerRepository.AddNewAddress(_mapper.Map<AddressInsertViewModel, Address>(viewModel));
        }
    }
}
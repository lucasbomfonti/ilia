using AutoMapper;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Domain;

namespace Ilia.CrossCutting.Mapper
{
    public class ViewModel2Domain : Profile
    {
        public ViewModel2Domain()
        {
            CreateMap<CustomerInsertViewModel, Customer>();
            CreateMap<CustomerUpdateViewModel, Customer>();

            CreateMap<CustomerPhoneContactInsertViewModel, PhoneContact>();
            CreateMap<PhoneContactInsertViewModel, PhoneContact>();

            CreateMap<CustomerAddressInsertViewModel, Address>();
            CreateMap<AddressInsertViewModel, Address>();

            CreateMap<UserInsertViewModel, User>();
            CreateMap<UserUpdateViewModel, User>();
        }
    }
}
using AutoMapper;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.Dto.Customer;
using Ilia.CrossCutting.Interop.Dto.User;
using Ilia.Domain;

namespace Ilia.CrossCutting.Mapper
{
    public class Domain2Dto : Profile
    {
        public Domain2Dto()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<PhoneContact, PhoneContactDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<ResponseDto<Customer>, ResponseDto<CustomerDto>>();
            CreateMap<ResponseDto<User>, ResponseDto<UserDto>>();
            CreateMap<User, UserDto>();
        }
    }
}
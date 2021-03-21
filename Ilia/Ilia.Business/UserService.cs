using Ilia.Business.Base;
using Ilia.Business.Contracts;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Helper;
using Ilia.Data.Repository.Contracts;
using Ilia.Data.RepositoryReadOnly.Contracts;
using Ilia.Domain;
using System;
using System.Threading.Tasks;

namespace Ilia.Business
{
    public class UserService : BaseService<User, BaseFilter>, IUserService
    {
        public UserService(IUserRepositoryReadOnly baseRepositoryReadOnly, IUserRepository baseRepository) : base(baseRepositoryReadOnly, baseRepository)
        {
        }

        public override Task<Guid> Create(User dto)
        {
            dto.Password = EncryptHelper.EncryptPassword(dto.Username, dto.Password);
            return base.Create(dto);
        }

        public async Task<User> Login(string login, string password)
        {
            return await ((IUserRepositoryReadOnly)BaseRepositoryReadOnly).Login(login, password);
        }
    }
}
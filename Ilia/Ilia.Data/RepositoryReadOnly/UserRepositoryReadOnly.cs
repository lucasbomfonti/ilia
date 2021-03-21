using Ilia.CrossCutting.Exceptions;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Helper;
using Ilia.Data.Context;
using Ilia.Data.RepositoryReadOnly.Base;
using Ilia.Data.RepositoryReadOnly.Contracts;
using Ilia.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ilia.Data.RepositoryReadOnly
{
    public class UserRepositoryReadOnly : BaseRepositoryReadOnly<User, BaseFilter>, IUserRepositoryReadOnly
    {
        public UserRepositoryReadOnly(DataContext context) : base(context)
        {
        }

        public async Task<User> Login(string name, string password)
        {
            password = EncryptHelper.EncryptPassword(name, password);
            var response = await Context.Set<User>().FirstOrDefaultAsync(w => w.Active && w.Username.Equals(name) && w.Password.Equals(password)) ?? throw new NotFoundException("username or password is invalid");
            return ExtractFromContext(response);
        }
    }
}
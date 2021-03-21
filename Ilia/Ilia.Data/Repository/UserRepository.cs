using Ilia.Data.Context;
using Ilia.Data.Repository.Base;
using Ilia.Data.Repository.Contracts;
using Ilia.Domain;

namespace Ilia.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
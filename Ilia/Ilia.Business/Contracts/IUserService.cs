using System.Threading.Tasks;
using Ilia.Business.Contracts.Base;
using Ilia.CrossCutting.Filter.Base;
using Ilia.Domain;

namespace Ilia.Business.Contracts
{
    public interface IUserService : IBaseService<User, BaseFilter>
    {
        Task<User> Login(string login, string password);
    }
}
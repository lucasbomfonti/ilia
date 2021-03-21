using Ilia.CrossCutting.Filter.Base;
using Ilia.Data.RepositoryReadOnly.Contracts.Base;
using Ilia.Domain;
using System.Threading.Tasks;

namespace Ilia.Data.RepositoryReadOnly.Contracts
{
    public interface IUserRepositoryReadOnly : IBaseRepositoryReadOnly<User, BaseFilter>
    {
        Task<User> Login(string name, string password);
    }
}
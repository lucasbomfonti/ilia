using Ilia.CrossCutting.Filter;
using Ilia.Data.RepositoryReadOnly.Contracts.Base;
using Ilia.Domain;
using System.Threading.Tasks;

namespace Ilia.Data.RepositoryReadOnly.Contracts
{
    public interface ICustomerRepositoryReadOnly : IBaseRepositoryReadOnly<Customer, CustomerFilter>
    {
        Task<Customer> GetCustomerByEmail(string email);
    }
}
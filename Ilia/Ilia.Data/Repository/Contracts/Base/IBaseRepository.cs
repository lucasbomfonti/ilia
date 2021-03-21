using Ilia.Data.Context;
using System;
using System.Threading.Tasks;

namespace Ilia.Data.Repository.Contracts.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<Guid> Create(T dto);

        Task<T> Update(T dto);

        Task Remove(Guid id);

        DataContext GetContext();
    }
}
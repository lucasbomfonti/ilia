using Ilia.Business.Contracts;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Context;
using Ilia.Data.Repository.Contracts.Base;
using Ilia.Data.RepositoryReadOnly.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ilia.Business.Contracts.Base;
using Ilia.Data.RepositoryReadOnly.Contracts.Base;

namespace Ilia.Business.Base
{
    public class BaseService<T, TT> : IBaseService<T, TT> where T : class where TT : BaseFilter
    {
        protected readonly IBaseRepository<T> BaseRepository;
        protected readonly IBaseRepositoryReadOnly<T, TT> BaseRepositoryReadOnly;

        public BaseService(IBaseRepositoryReadOnly<T, TT> baseRepositoryReadOnly, IBaseRepository<T> baseRepository)
        {
            BaseRepositoryReadOnly = baseRepositoryReadOnly;
            BaseRepository = baseRepository;
        }

        public virtual async Task<Guid> Create(T viewModel)
        {
            return await BaseRepository.Create(viewModel);
        }

        public virtual async Task<T> Update(T viewModel)
        {
            return await BaseRepository.Update(viewModel);
        }

        public virtual async Task Remove(Guid id)
        {
            await BaseRepository.Remove(id);
        }

        public virtual async Task<T> Find(Guid id)
        {
            return await BaseRepositoryReadOnly.Find(id);
        }

        public virtual async Task<List<T>> All()
        {
            return await BaseRepositoryReadOnly.All();
        }

        public virtual async Task<ResponseDto<T>> Search(RequestViewModel<TT> request)
        {
            return await BaseRepositoryReadOnly.Search(request);
        }

        public DataContext GetContext()
        {
            return BaseRepository.GetContext();
        }
    }
}
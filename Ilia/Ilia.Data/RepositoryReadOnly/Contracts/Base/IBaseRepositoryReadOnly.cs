using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ilia.Data.RepositoryReadOnly.Contracts.Base
{
    public interface IBaseRepositoryReadOnly<T, TT> where T : class where TT : BaseFilter
    {
        Task<T> Find(Guid id);

        Task<List<T>> All();

        Task<ResponseDto<T>> Search(RequestViewModel<TT> request);

        DataContext GetContext();
    }
}
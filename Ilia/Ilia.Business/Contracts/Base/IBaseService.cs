using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ilia.Business.Contracts.Base
{
    public interface IBaseService<T, TF> where T : class where TF : BaseFilter
    {
        Task<Guid> Create(T viewModel);

        Task<T> Update(T viewModel);

        Task Remove(Guid id);

        Task<T> Find(Guid id);

        Task<List<T>> All();

        Task<ResponseDto<T>> Search(RequestViewModel<TF> request);

        DataContext GetContext();
    }
}
using AutoMapper;
using Ilia.Business.Contracts.Base;
using Ilia.CrossCutting.Extensions;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Base;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ilia.Api.Controllers.Base
{
    public class BaseController<T, TF, TDto, TListDto, TInsertViewModel, TUpdateViewModel> : ControllerBase where T : class
        where TF : BaseFilter
        where TListDto : class
        where TDto : class
        where TInsertViewModel : class
        where TUpdateViewModel : BaseUpdateViewModel
    {
        protected readonly IBaseService<T, TF> BaseService;
        private readonly IMapper _mapper;

        public BaseController(IBaseService<T, TF> baseService, IMapper mapper)
        {
            BaseService = baseService;
            _mapper = mapper;
        }

        protected async Task<ActionResult> Post(TInsertViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var response = await BaseService.Create(_mapper.Map<TInsertViewModel, T>(viewModel));

            return await Response(response, HttpStatusCode.Created);
        }

        protected async Task<ActionResult> Update(TUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var response = await BaseService.Update(_mapper.Map<TUpdateViewModel, T>(viewModel));

            return await Response(_mapper.Map<T, TDto>(response), HttpStatusCode.OK);
        }

        protected async Task<ActionResult> Delete(Guid id)
        {
            await BaseService.Remove(id);
            return await Response(new object(), HttpStatusCode.NoContent);
        }

        protected async Task<ActionResult> Find(Guid id) => await Response(_mapper.Map<T, TDto>(await BaseService.Find(id)), HttpStatusCode.OK);

        protected async Task<ActionResult> Get(int? page = null, int? perPage = null, object filter = null)
        {
            var response = await BaseService.Search(new RequestViewModel<TF>(page, perPage, (TF)filter));
            return await Response(_mapper.Map<ResponseDto<T>, ResponseDto<TListDto>>(response), HttpStatusCode.OK);
        }

        protected virtual async Task<ActionResult> Response(object data, HttpStatusCode code) => await Task.FromResult(new ObjectResult(data) { StatusCode = (int)code });
    }
}
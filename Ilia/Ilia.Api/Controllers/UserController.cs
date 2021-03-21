using AutoMapper;
using Ilia.Api.Controllers.Base;
using Ilia.Business.Contracts;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Dto.User;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Ilia.Api.Controllers
{
    [Route("api/v1/users")]
    public class UserController : BaseController<User, BaseFilter, UserDto, UserDto, UserInsertViewModel,
        UserUpdateViewModel>
    {
        public UserController(IUserService service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get()
            => await base.Get();

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public new async Task<ActionResult> Post([FromBody] UserInsertViewModel viewModel) =>
            await base.Post(viewModel);
    }
}
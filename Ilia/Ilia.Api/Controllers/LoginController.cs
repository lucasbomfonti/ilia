using AutoMapper;
using Ilia.Api.Controllers.Base;
using Ilia.Api.Security;
using Ilia.Business.Contracts;
using Ilia.CrossCutting.Extensions;
using Ilia.CrossCutting.Filter.Base;
using Ilia.CrossCutting.Interop.Dto.User;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Ilia.Api.Controllers
{
    [Route("api/v1/login")]
    public class LoginController : BaseController<User, BaseFilter, UserDto, UserDto, UserInsertViewModel, UserUpdateViewModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoginController(IUserService service, IMapper mapper) : base(service, mapper)
        {
            _userService = service;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            return Ok(UserManagement.RegisterUser(_mapper.Map<User, UserDto>(await _userService.Login(model.Name, model.Password))));
        }
    }
}
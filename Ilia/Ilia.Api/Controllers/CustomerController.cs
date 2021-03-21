using AutoMapper;
using Ilia.Api.Controllers.Base;
using Ilia.Business.Contracts;
using Ilia.CrossCutting.Filter;
using Ilia.CrossCutting.Interop.Dto.Customer;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ilia.Api.Security;

namespace Ilia.Api.Controllers
{
    [Route("api/v1/customer")]
    public class CustomerController : BaseController<Customer, CustomerFilter, CustomerDto, CustomerDto, CustomerInsertViewModel, CustomerUpdateViewModel>
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService service, IMapper mapper) : base(service, mapper)
        {
            _customerService = service;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public new async Task<ActionResult> Find(Guid id) => await base.Find(id);

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Get([FromQuery] CustomerFilter filter)
        {
            return await base.Get(filter.Page, filter.PerPage, filter);
        }

        [HttpPost]
        [AccessValidation]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public new async Task<ActionResult> Post([FromBody] CustomerInsertViewModel viewModel) => await base.Post(viewModel);

        [HttpPut]
        [AccessValidation]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Put([FromBody] CustomerUpdateViewModel viewModel) => await Update(viewModel);

        [HttpDelete("{id}")]
        [AccessValidation]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Remove(Guid id) => await Delete(id);

        [HttpPost("phone-contact")]
        [AccessValidation]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> PostPhoneContact([FromBody] PhoneContactInsertViewModel viewModel)
        {
            return await base.Response(await _customerService.CreatePhoneContact(viewModel), HttpStatusCode.Created);
        }

        [HttpPost("address")]
        [AccessValidation]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> PostAddress([FromBody] AddressInsertViewModel viewModel)
        {
            return await base.Response(await _customerService.CreateAddress(viewModel), HttpStatusCode.Created);
        }
    }
}
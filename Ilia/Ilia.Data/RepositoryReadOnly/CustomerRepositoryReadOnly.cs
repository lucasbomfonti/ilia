using Ilia.CrossCutting.Exceptions;
using Ilia.CrossCutting.Filter;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Context;
using Ilia.Data.RepositoryReadOnly.Base;
using Ilia.Data.RepositoryReadOnly.Contracts;
using Ilia.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilia.Data.RepositoryReadOnly
{
    public class CustomerRepositoryReadOnly : BaseRepositoryReadOnly<Customer, CustomerFilter>, ICustomerRepositoryReadOnly
    {
        public CustomerRepositoryReadOnly(DataContext context) : base(context)
        {
        }

        public override async Task<Customer> Find(Guid id)
        {
            return await Context
                       .Set<Customer>()
                       .Include(f => f.PhoneContact)
                       .Include(f => f.Address)
                       .FirstOrDefaultAsync(f => f.Id.Equals(id) && f.Active)
                       ?? throw new NotFoundException("customer not found");
        }

        public override async Task<List<Customer>> All()
        {
            return await Task.FromResult(
                Context
                    .Set<Customer>()
                    .Include(f => f.PhoneContact)
                    .Include(f => f.Address)
                    .Where(w => w.Active)
                    .ToList());
        }

        public override async Task<ResponseDto<Customer>> Search(RequestViewModel<CustomerFilter> request)
        {
            var query = Context
                .Set<Customer>()
                .Include(f => f.PhoneContact)
                .Include(f => f.Address)
                .Where(w => w.Active).AsQueryable();

            query = Filter(request, query, Context);

            var total = query.Select(s => 1).Count();
            var skip = request.Page > 1 ? (request.Page - 1) * request.PerPage : 0;

            if (total <= request.PerPage)
            {
                skip = 0;
                request.Page = 1;
            }

            var temp = query.Skip(skip).Take(request.PerPage);

            return await Task.FromResult(new ResponseDto<Customer>
            {
                CurrentPage = request.Page,
                Data = ExtractFromContext(temp.ToList()),
                PerPage = request.PerPage,
                Total = total
            });
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await Context.Customer.FirstOrDefaultAsync(f => f.Email.Equals(email) && f.Active);
        }

        protected override IQueryable<Customer> Filter(RequestViewModel<CustomerFilter> request, IQueryable<Customer> query, DataContext context)
        {
            if (!string.IsNullOrEmpty(request.Filter.Country))
                query = query.Where(w => w.Address.Any(a => a.Country.Equals(request.Filter.Country)));

            if (!string.IsNullOrEmpty(request.Filter.State))
                query = query.Where(w => w.Address.Any(a => a.State.Equals(request.Filter.State)));

            if (!string.IsNullOrEmpty(request.Filter.City))
                query = query.Where(w => w.Address.Any(a => a.City.Equals(request.Filter.City)));

            if (!string.IsNullOrEmpty(request.Filter.ZipCode))
                query = query.Where(w => w.Address.Any(a => a.ZipCode.Equals(request.Filter.ZipCode)));

            if (!string.IsNullOrEmpty(request.Filter.Street))
                query = query.Where(w => w.Address.Any(a => a.Street.Equals(request.Filter.Street)));

            return base.Filter(request, query, context);
        }
    }
}
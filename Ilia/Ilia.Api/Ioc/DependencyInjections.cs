using Ilia.Business;
using Ilia.Business.Contracts;
using Ilia.Data.Context;
using Ilia.Data.Repository;
using Ilia.Data.Repository.Contracts;
using Ilia.Data.RepositoryReadOnly;
using Ilia.Data.RepositoryReadOnly.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Ilia.Api.Ioc
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerRepositoryReadOnly, CustomerRepositoryReadOnly>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepositoryReadOnly, UserRepositoryReadOnly>();
        }
    }
}
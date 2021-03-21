using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ilia.CrossCutting.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Ilia.Api.Ioc
{
    public static class MapperConfig
    {
        public static void RegisterMappings(this IServiceCollection service)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Domain2Dto());
                mc.AddProfile(new ViewModel2Domain());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}

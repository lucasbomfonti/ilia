using AutoMapper;
using Ilia.CrossCutting.Mapper;
using Ilia.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace Ilia.Api.Test.Helper
{
    public static class TestHelper
    {
        public static IMapper GetMapper()
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Domain2Dto());
                mc.AddProfile(new ViewModel2Domain());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }

        public static T ConvertFromActionResult<T>(ActionResult actionResult) where T : class
        {
            var text = JsonConvert.SerializeObject(actionResult);
            try
            {
                var converted = JsonConvert.DeserializeObject<ResponseDto>(text);
                var parssed = JsonConvert.DeserializeObject<T>(converted.Value.Data.ToString());
                return parssed;
            }
            catch (Exception)
            {
                var converted = JsonConvert.DeserializeObject<ResponseTypeDto<T>>(text);
                return converted.Value;
            }
        }

        public static void PrepareDatabase<T>() where T : DataContext, new()
        {
            var context = new T();
            context.UpdateDatabase();
        }
    }

    public class ResponseTypeDto<T> where T : class
    {
        public T Value { get; set; }
    }

    public class ResponseDto
    {
        public ResponseData Value { get; set; }
    }

    public class ResponseData
    {
        public object Data { get; set; }
    }
}
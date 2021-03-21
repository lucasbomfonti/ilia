using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Ilia.CrossCutting.Extensions
{
    public static class MapperHelper
    {
        public static IEnumerable<TDestiny> CopyList<TSource, TDestiny>(this IMapper mapper, IEnumerable<TSource> src)
        {
            var ret = new List<TDestiny>();
            if (src == null)
            {
                return ret;
            }

            ret.AddRange(src.Select(origin => (TDestiny)mapper.Map(origin, typeof(TSource), typeof(TDestiny))));
            return ret;
        }

        public static TDestiny Map<TSource, TDestiny>(this IMapper mapper, TSource origin)
        {
            return mapper.Map<TDestiny>(origin);

        }
    }
}

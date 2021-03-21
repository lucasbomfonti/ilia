using Ilia.CrossCutting.Exceptions;
using Ilia.Data.Context;
using Ilia.Data.Repository.Contracts.Base;
using Ilia.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Ilia.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;

        public BaseRepository(DataContext context)
        {
            Context = context;
        }

        public virtual async Task<Guid> Create(T dto)
        {
            await Context.Set<T>().AddAsync(dto);
            await Context.SaveChangesAsync();
            return dto.Id;
        }

        public virtual async Task<T> Update(T dto)
        {
            var currentValue = SetValue(dto);
            await Context.SaveChangesAsync();
            return ExtractFromContext(currentValue);
        }

        public virtual async Task Remove(Guid id)
        {
            var obj = await Context.Set<T>().FirstAsync(f => f.Id.Equals(id));
            obj.Active = false;
            await Context.SaveChangesAsync();
        }

        public DataContext GetContext()
        {
            return new DataContext();
        }

        protected virtual void SetValue(T obj, T currentValue)
        {
            obj.Version++;
            Context.Entry(currentValue).CurrentValues.SetValues(obj);
        }

        private T SetValue(T obj)
        {
            var currentValue = Context.Set<T>().Find(obj.Id);
            if (currentValue?.Version != obj.Version)
                throw new VersionException("Record outdated, update and try again");

            SetValue(obj, currentValue);
            currentValue.Date = DateTime.Now;
            return currentValue;
        }

        protected TE ExtractFromContext<TE>(TE dto)
        {
            var temp = JsonConvert.SerializeObject(dto);
            return JsonConvert.DeserializeObject<TE>(temp);
        }
    }
}
using Microsoft.Extensions.Caching.Memory;
using TripXTest.Application.Contracts;
using TripXTest.Core.Entities;

namespace TripXTest.Infrastructure
{
    public class TripXContext<T> : ITripXContext<T> where T : BaseEntity
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _defaultOptions;

        public TripXContext(IMemoryCache cache)
        {
            _cache = cache;
            _defaultOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            };
        }

        public void Save(T entity)
        {
            T saved = _cache.Set(entity.Code, entity, _defaultOptions);
        }

        public void SaveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Save(entity);
            }
        }

        public T Get(Guid uid)
        {
            return _cache.Get<T>(uid)!;
        }

        public T Get(string code)
        {
            return _cache.Get<T>(code)!;
        }
    }
}

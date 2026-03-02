using TripXTest.Core.Entities;

namespace TripXTest.Application.Contracts
{
    public interface ITripXContext<T> where T : BaseEntity
    {
        public void Save(T entity);

        void SaveRange(IEnumerable<T> entities);

        public T Get(Guid uid);

        public T Get(string code);
    }
}

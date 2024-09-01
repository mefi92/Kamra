using Kamra.Core.Interfaces;
using System.Net.Http.Headers;

namespace Karma.Persistence
{
    public class InMemoryPersistence<T> : IPersistence<T>
    {
        private readonly List<T> _entities = new List<T>();

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public T GetByName(string name)
        {
            return _entities.FirstOrDefault(e => e.GetType().GetProperty("Name").GetValue(e, null).ToString() == name);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsReadOnly();
        }        

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void Update(T entity)
        {
            var existingEntity = GetByName(entity.GetType().GetProperty("Name").GetValue(entity, null).ToString());
            if (existingEntity != null)
            {
                _entities.Remove(existingEntity);
                _entities.Add(entity);
            }
        }
    }
}

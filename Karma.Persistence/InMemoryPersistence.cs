using Kamra.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            return _entities.FirstOrDefault(e => GetEntityName(e) == name);
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
            var existingEntity = GetByName(GetEntityName(entity));
            if (existingEntity != null)
            {
                int index = _entities.IndexOf(existingEntity);
                _entities[index] = entity;
            }
        }

        private string GetEntityName(T entity)
        {
            return entity.GetType().GetProperty("Name")?.GetValue(entity, null)?.ToString();
        }
    }
}

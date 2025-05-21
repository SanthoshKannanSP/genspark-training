//using assignment_2.Exceptions;
using assignment_2.Interfaces;

namespace assignment_2.Repositories
{
    public abstract class AbstractRepository<K, T> : IRepository<K, T> where T : class
    {
        protected List<T> _items = new List<T>();
        public abstract ICollection<T> GetAll();
        public abstract T GetById(K id);

        public T Add(T item)
        {
            _items.Add(item);
            return item;
        }

        public T Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            _items.Remove(item);
            return item;
        }

        public T Update(T item)
        {
            var myItem = GetById((K)item.GetType().GetProperty("Id").GetValue(item));
            if (myItem == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            var index = _items.IndexOf(myItem);
            _items[index] = item;
            return item;
        }
    }
}

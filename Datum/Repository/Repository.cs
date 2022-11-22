using System;
using System.Data.Entity;
using System.Linq;

namespace Repository
{
    public class Repository<T>
        : IDisposable, IRepository<T> where T : class
    {
        private Context _context;
        public Repository(Context context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        void IRepository<T>.Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        void IRepository<T>.Edit(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        T IRepository<T>.Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        IQueryable<T> IRepository<T>.List()
        {
            return _context.Set<T>();
        }

        void IRepository<T>.Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }
    }
}

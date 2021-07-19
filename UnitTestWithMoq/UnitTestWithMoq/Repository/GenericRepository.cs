using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestWithMoq.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context = null;

        /// <summary>
        /// as the class here is Generic so we can't access the model directly
        /// so creating a DbSet depending on the T generic class
        /// </summary>
        private DbSet<T> table = null;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

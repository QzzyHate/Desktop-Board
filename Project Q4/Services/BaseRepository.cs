using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Services
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context) => _context = context;

        public virtual IEnumerable<T> GetAll() => [.. _context.Set<T>()];
        public virtual T GetById(int id) => _context.Set<T>().Find(id);
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
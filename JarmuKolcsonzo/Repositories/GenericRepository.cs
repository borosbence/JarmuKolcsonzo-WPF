using JarmuKolcsonzo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarmuKolcsonzo.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> 
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext _context;
        public GenericRepository(TContext context)
        {
            this._context = context;
        }
        public virtual void Add(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public virtual TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);   
        }

        public virtual List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}

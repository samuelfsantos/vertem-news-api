using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vertem.News.Domain.Interfaces;
using Vertem.News.Infra.Base;
using Vertem.News.Infra.Data.Contexts;

namespace Vertem.News.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public virtual IQueryable<T> ObterTodos()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public virtual IQueryable<T> Consultar(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual Task<T> ObterAsync(Guid id)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual T Adicionar(T entity)
        {
            var entityResult = _dbSet.Add(entity);

            return entityResult.Entity;
        }

        public virtual T Modificar(T entity)
        {
            var entityResult = _dbSet.Update(entity);

            return entityResult.Entity;
        }

        public virtual void Remover(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}

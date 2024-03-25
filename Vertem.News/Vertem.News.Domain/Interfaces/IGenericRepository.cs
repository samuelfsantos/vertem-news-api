using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vertem.News.Infra.Base;

namespace Vertem.News.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        IQueryable<T> ObterTodos();
        IQueryable<T> Consultar(Expression<Func<T, bool>> predicate);
        Task<T> ObterAsync(Guid id);
        T Adicionar(T entity);
        T Modificar(T entity);
        void Remover(T entity);
    }
}

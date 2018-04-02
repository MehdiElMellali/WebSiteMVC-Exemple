using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobOffers.Repository
{
    public interface IRepository<T> where T :class
    {
        IEnumerable<T> getAll();
        IEnumerable<T> getBy(Expression<Func<T,bool>>predicate);
        T create(T entity);
        T update(T entity);
        void delete(Expression<Func<T, bool>> predicate);
    }
}

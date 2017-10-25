using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(int id);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        bool Update(TEntity entity);

        bool Remove(TEntity entity);
        bool Remove(int id);
    }
}

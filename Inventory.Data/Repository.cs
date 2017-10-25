using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Inventory.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Fields

        private InventoryEntities _context;
        private IDbSet<TEntity> _entity;

        #endregion

        #region Builder

        public Repository()
        {
            this._context = new InventoryEntities();
            this._entity = this._context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public TEntity Add(TEntity entity)
        {
            TEntity res = this._entity.Add(entity);
            this._context.SaveChanges();
            return res;
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return this._entity.Where(predicate).ToList();
        }

        public TEntity Find(int id)
        {
            return this._entity.Find(id);
        }

        public IEnumerable<TEntity> Get()
        {
            return this._entity.ToList();
        }

        public bool Remove(TEntity entity)
        {
            this._entity.Remove(entity);
            this._context.SaveChanges();
            return true;
        }

        public bool Remove(int id)
        {
            return this.Remove(this.Find(id));
        }

        public bool Update(TEntity entity)
        {
            this._entity.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
            return true;
        }

        #endregion
    }
}

using LoadPagesSpeedTest.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Repository
{
    public class BaseEFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDBContext db;
        protected readonly DbSet<TEntity> dbSet;

        public BaseEFRepository(AppDBContext context)
        {
            this.db = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            dbSet.Add(entity);
            await db.SaveChangesAsync();
            return 0;
        }

        public virtual async Task Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            await db.SaveChangesAsync();
        }

        public virtual async Task<TEntity> FindById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetEntitiesByParentId(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Update(TEntity entity)
        {
            dbSet.AddOrUpdate(entity);
            await db.SaveChangesAsync();
        }
    }
}
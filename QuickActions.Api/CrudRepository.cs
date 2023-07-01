using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuickActions.Api
{
    public class CrudRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public CrudRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public Task Create(TEntity entity)
        {
            return Create(new[] { entity });
        }

        public async Task Create(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> Read(Expression<Func<TEntity, bool>> expression)
        {
            return (await Read(expression, 0, 1)).FirstOrDefault(); ;
        }

        public Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> expression, int start, int skip)
        {
            return dbSet.Where(expression).Skip(start).Take(skip).AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Expression<Func<TEntity, bool>> expression)
        {
            var entitiesToRemove = await Read(expression, 0, int.MaxValue);
            dbSet.RemoveRange(entitiesToRemove);
            await dbContext.SaveChangesAsync();
        }
    }
}
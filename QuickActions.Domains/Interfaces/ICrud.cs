using QuickActions.Common.Specifications;
using Refit;

namespace QuickActions.Common.Interfaces
{
    public interface ICrud<TEntity> where TEntity : class
    {
        [Post("")]
        public Task Create([Body]  TEntity entity);
        [Post("/Many")]
        public Task Create([Body] IEnumerable<TEntity> entities);
        [Get("")]
        public Task<TEntity> Read([Body] ISpecification<TEntity> specification);
        [Get("/Many")]
        public Task<List<TEntity>> Read([Body] ISpecification<TEntity> specification, [Query] int start, [Query] int skip);
        [Patch("")]
        public Task Update([Body] TEntity entity);
        [Delete("")]
        public Task Delete([Body] ISpecification<TEntity> specification);
    }
}
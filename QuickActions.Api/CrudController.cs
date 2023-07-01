using Microsoft.AspNetCore.Mvc;
using QuickActions.Common.Interfaces;
using QuickActions.Common.Specifications;

namespace QuickActions.Api
{
    public class CrudController<TEntity> : ControllerBase, ICrud<TEntity> where TEntity : class
    {
        private readonly CrudRepository<TEntity> repository;

        public CrudController(CrudRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpPost("")]
        public async Task Create([FromBody] TEntity entity)
        {
            await repository.Create(entity);
        }

        [HttpPost("Many")]
        public async Task Create([FromBody] IEnumerable<TEntity> entities)
        {
            await repository.Create(entities);
        }

        [HttpGet("")]
        public async Task<TEntity> Read([FromBody] ISpecification<TEntity> specification)
        {
            return await repository.Read(specification.ToExpression());
        }

        [HttpGet("Many")]
        public async Task<List<TEntity>> Read([FromBody] ISpecification<TEntity> specification, [FromQuery] int start, [FromQuery] int skip)
        {
            return await repository.Read(specification.ToExpression(), start, skip);
        }

        [HttpPatch("")]
        public async Task Update([FromBody] TEntity entity)
        {
            await repository.Update(entity);
        }

        [HttpDelete("")]
        public async Task Delete([FromBody] ISpecification<TEntity> specification)
        {
            await repository.Delete(specification.ToExpression());
        }
    }
}
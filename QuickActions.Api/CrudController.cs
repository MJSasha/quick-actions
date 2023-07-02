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

        [HttpPost("Read")]
        public async Task<TEntity> Read([FromBody] Specification<TEntity> specification)
        {
            return await repository.Read(specification);
        }

        [HttpPost("Read/Many")]
        public async Task<List<TEntity>> Read([FromBody] Specification<TEntity> specification, [FromQuery] int start, [FromQuery] int skip)
        {
            return await repository.Read(specification, start, skip);
        }

        [HttpPatch("")]
        public async Task Update([FromBody] TEntity entity)
        {
            await repository.Update(entity);
        }

        [HttpDelete("")]
        public async Task Delete([FromBody] Specification<TEntity> specification)
        {
            await repository.Delete(specification);
        }
    }
}
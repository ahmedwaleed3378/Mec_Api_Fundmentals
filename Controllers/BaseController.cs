using Mec_Api_Fundmentals.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mec_Api_Fundmentals.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController<T> : ControllerBase where T : class
	{
		private readonly IGenericRepository<T> _repository;


		public BaseController(IGenericRepository<T> repository)
		{
			_repository = repository;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<T>>> GetAll()
		{
			var entities = await _repository.GetAllAsync();
			return Ok(entities);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<T>>> GetAll(int id)
		{
			var entity = await _repository.GetByIdAsync(id);
			return Ok(entity);
		}

		[HttpPost]
		public async Task<ActionResult<T>> Create(T entity)
		{
			var createdEntity = await _repository.CreateAsync(entity);
			return CreatedAtAction(nameof(GetEntityId), new { id = GetEntityId(createdEntity) }, createdEntity);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<T>> Update(int id, T entity)
		{
			var updatedEntity = await _repository.UpdateAsync(id, entity);
			return Ok(updatedEntity);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _repository.DeleteAsync(id);
			if (!result) return NotFound();
			return NoContent();
		}

		protected virtual object GetEntityId(T entity)
		{
			// Assumes your entities have an Id property
			return entity.GetType().GetProperty("Id")?.GetValue(entity);
		}
	}
}


using Mec_Api_Fundmentals.Core;
using Microsoft.EntityFrameworkCore;

namespace Mec_Api_Fundmentals.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly EcommerceDbContext _context;
		private readonly DbSet<T> _entities;

		public GenericRepository(EcommerceDbContext context)
		{
			_context = context;
			_entities = context.Set<T>();
		}

		public async Task<T> CreateAsync(T entity)
		{
			_entities.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _entities.FindAsync(id);
			if (entity == null)
			{
				return false;
			}
			_entities.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _entities.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _entities.FindAsync(id);
		}

		public async Task<T> UpdateAsync(int id, T entity)
		{
			var tempEntity = await _entities.FindAsync(id);
			tempEntity = entity;
			await _context.SaveChangesAsync();
			return tempEntity;
		}
	}
}

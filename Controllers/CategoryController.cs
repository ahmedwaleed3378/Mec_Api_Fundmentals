using Mec_Api_Fundmentals.Core;
using Mec_Api_Fundmentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mec10th.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly EcommerceDbContext _context;

		public CategoryController(EcommerceDbContext context)
		{
			_context = context;
		}

		// GET: api/Category
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _context.Categories.ToListAsync();
		}

		// GET: api/Category/5
		[HttpGet("getCategoryById")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			return category;
		}

		// POST: api/Category


		[HttpPost]
		public async Task<ActionResult> PostCategory(string categoryName)
		{
			if (categoryName == null)
			{
				return BadRequest("Category cannot be null.");
			}
			var cat = new Category()
			{
				Name = categoryName,
				Products = new List<Product>()
			};
			_context.Categories.Add(cat);
			await _context.SaveChangesAsync();
			return Ok(new { message = "Category is added successfully", category = categoryName });
		}






		//[HttpPost]
		//public async Task<ActionResult<Category>> PostCategory(Category category)
		//{
		//    _context.Categories.Add(category);
		//    await _context.SaveChangesAsync();
		//    return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
		//}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}
			_context.Entry(category).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				throw;
			}
			return NoContent();
		}

		// DELETE: api/Category/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		private bool CategoryExists(int id)
		{
			return _context.Categories.Any(e => e.Id == id);
		}




	}
}

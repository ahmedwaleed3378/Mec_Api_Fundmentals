using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Mec_Api_Fundmentals.Core;
using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Models.DTOs;

namespace Mec9th.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly EcommerceDbContext _context;

		public ProductController(EcommerceDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
		{
			var products = await _context.Products.Include(p => p.Category).ToListAsync();
			var productsDto = products.Select(entity => Mapper.ToDto(entity));
			return Ok(new { message = "Success", data = productsDto });
		}





		[HttpPost]
		public async Task<ActionResult> CreateProduct(PostProductDto productDto)
		{
			if (productDto == null)
				return BadRequest();


			var product = Mapper.PostEntity(productDto);
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
			return Ok("added sucessfully");
		}
		// PUT: api/Product/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}
			_context.Entry(product).State = EntityState.Modified;
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
				{
					return NotFound();
				}
				throw;
			}
			return NoContent();
		}

		// DELETE: api/Product/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
			return Ok($"Deleted Where Id = {id}");
		}

		private bool ProductExists(int id)
		{
			return _context.Products.Any(e => e.Id == id);
		}
	}


}

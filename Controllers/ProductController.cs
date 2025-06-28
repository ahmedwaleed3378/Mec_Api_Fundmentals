using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Mec_Api_Fundmentals.Core;
using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Models.DTOs;
using Mec_Api_Fundmentals.Models.Exceptions;

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
		public async Task<ActionResult<ApiResponse<IEnumerable<GetProductDto>>>> GetProducts()
		{
			var products = await _context.Products.Include(p => p.Category).ToListAsync();
			var productsDto = products.Select(entity => Mapper.ToDto(entity)).ToList();
			return Ok(ApiResponse<IEnumerable<GetProductDto>>.CreateSuccess(productsDto));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ApiResponse<GetProductDto>>> GetById(int id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				throw new ApiException("Product is not found", StatusCodes.Status404NotFound);
			}
			return Ok(ApiResponse<GetProductDto>.CreateSuccess(Mapper.ToDto(product)));
		}


		[HttpPost]
		public async Task<ActionResult<ApiResponse<GetProductDto>>> CreateProduct(PostProductDto productDto)
		{
			if (productDto == null)
				throw new ApiException("Product data is required", StatusCodes.Status400BadRequest);

			var product = Mapper.PostEntity(productDto);
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			var createdProductDto = Mapper.ToDto(product);
			return Ok(ApiResponse<GetProductDto>.CreateSuccess(createdProductDto, "Product added successfully"));
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ApiResponse<GetProductDto>>> PutProduct(int id, Product product)
		{
			if (id != product.Id)
				throw new ApiException("ID mismatch", StatusCodes.Status400BadRequest);

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
				return Ok(ApiResponse<GetProductDto>.CreateSuccess(
					Mapper.ToDto(product),
					"Product updated successfully"));
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
					throw new ApiException($"Product with ID {id} not found", StatusCodes.Status404NotFound);
				throw;
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ApiResponse<object>>> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id)
				?? throw new ApiException($"Product with ID {id} not found", StatusCodes.Status404NotFound);

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return Ok(ApiResponse<object>.CreateSuccess(null, $"Product with ID {id} deleted successfully"));
		}

		private bool ProductExists(int id)
		{
			return _context.Products.Any(e => e.Id == id);
		}
	}


}

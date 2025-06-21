using Mec_Api_Fundmentals.Models;
using Mec_Api_Fundmentals.Models.DTOs;

namespace Mec_Api_Fundmentals.Core
{
	public static class Mapper
	{

		public static Product PostEntity(PostProductDto productDto)
		{
			return new Product
			{
				Name = productDto.Name,
				Price = productDto.Price,
				Quantity = productDto.Quantity,
				CategoryId = productDto.CategoryId,
			};
		}


		public static GetProductDto ToDto(Product entity)
		{
			return new GetProductDto
			{
				ProductId = entity.Id,
				Name = entity.Name,
				Price = (entity.Quantity < 5 ? entity.Price + 1000.0m : entity.Price) ?? 0,
				Quantity = entity.Quantity ?? 0,
				CategoryId = entity.CategoryId ?? 0,
				CategoryName = entity.Category?.Name
			};


		}
	}


}

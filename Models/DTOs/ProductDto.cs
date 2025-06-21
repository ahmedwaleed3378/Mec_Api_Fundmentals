namespace Mec_Api_Fundmentals.Models.DTOs
{
	public class PostProductDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int CategoryId { get; set; }

	}

	public class GetProductDto
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
	}
}

namespace Mec_Api_Fundmentals.Models
{
	public partial class Category
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}

}

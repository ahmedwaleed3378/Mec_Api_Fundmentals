namespace Mec_Api_Fundmentals.Models
{
	public partial class Order
	{
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	}

}

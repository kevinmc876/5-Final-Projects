namespace InventorySystem.Models
{
    public class Products
	{
		public int Id { get; set; }
		public string? Name { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
		public int SizeId { get; set; }
		public int Quantity { get; set; }

		//Nav Properties
		public Categories? Category { get; set; }
		public Sizes? Size { get; set; }


	}
}

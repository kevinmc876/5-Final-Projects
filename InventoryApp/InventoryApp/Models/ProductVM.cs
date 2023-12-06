using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryInterface.Models
{
	public class ProductVM
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Image { get; set; }

		public int CategoryId { get; set; }
		public int sizeId { get; set; }

		//Dropdowns for Category & Size List
		public List<SelectListItem>? CategoryList { get; set; }	
		public List<SelectListItem>? SizeList { get; set;}

		public int Quantity { get; set; }

		//Selected Category & Size
		[Display(Name = "Category")]
		public int SelectedCategory {  get; set; }
		[Display(Name = "Size")]
		public int SelectedSize { get; set; }


	}
}

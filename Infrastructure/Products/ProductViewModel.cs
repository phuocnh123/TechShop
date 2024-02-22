using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Products
{
	public class ProductViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 0;
		public string Detail { get; set; }
		public string Description { get; set; }
		public Guid CategoryId { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string Image { get; set; } = string.Empty;
		public string CategoryName { get; set; }
        public int? Rating { get; set; }
    }
}

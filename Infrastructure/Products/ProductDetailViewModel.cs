using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Products
{
	public class ProductDetailViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 0;
		public string Detail { get; set; }
		public string Description { get; set; }
		public Guid CategoryId { get; set; }
		public decimal? DiscountPrice { get; set; }
		public List<ImageViewModel> Images { get; set; }
		public string CategoryName { get; set; }
		public List<ReviewModel> Reviews { get; set; }
	}

	public class ImageViewModel
	{
        public Guid Id { get; set; }
        public string ImageLink { get; set; }
        public string Alt { get; set; }
    }

	public class ReviewModel
	{
		public Guid Id { get; set; }
		public string ReviewerName { get; set; }
		public string? Email { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
	}
}

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Products
{

    public class ProductCreateViewModel
    {
        public Guid ? ProductId  { get; set; }

        [Required]
        public Guid  CategoryId  { get; set; }

        [Required]
        public string ProductName  { get; set; }

        public decimal Price  { get; set; }
        public decimal? DiscountPrice  { get; set; }

        [Required]
        public int Quantity  { get; set; }

        [Required]
        public string Detail  { get; set; }

        [Required]
        public string Description  { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();

    }
}

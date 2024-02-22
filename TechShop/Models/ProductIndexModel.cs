using Infrastructure.Categories;
using Infrastructure.Products;

namespace TechShop.Models
{
	public class ProductIndexModel
	{
        public List<CategoryViewModel> Categories { get; set; }
        public List<ProductOrderByModel> OrderBys { get; set; }
        public List<int> SelectPageSize { get; set; }
        public string CategoryId { get; set; }
        public string KeyWord { get; set; }
    }

    public class ProductOrderByModel
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }
}

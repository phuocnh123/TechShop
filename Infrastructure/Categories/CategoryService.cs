using Infrastructure.Entities;
using Infrastructure.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Categories
{

	public interface ICategoryService
	{
		List<CategoryViewModel> GetCategories();
	}
	public class CategoryService: ICategoryService
	{
		private readonly TechShopDbContext _context;

        public CategoryService(TechShopDbContext context)
        {
            _context = context;
        }

		public List<CategoryViewModel> GetCategories()
		{
			var categoryViewModels = new List<CategoryViewModel>();
			var productViewModels = new List<ProductViewModel>();
			var products = _context.Products.AsQueryable();
			var categories = _context.Categories.AsQueryable();
			var result = (from p in products
						 join c in categories
						 on p.CategoryId equals c.Id
						 select new ProductViewModel
						 {
							 Id = p.Id,
							 Name = p.Name,
							 Price = p.Price,
							 DiscountPrice = p.DiscountPrice,
							 Description = p.Description,
							 Detail = p.Detail,
							 CategoryName = c.Name,
							 CategoryId = c.Id,
						 }).AsEnumerable();
			var groups = result.GroupBy(s => s.CategoryId);	
			foreach ( var item in groups ) {

				var productsInGroup = item.ToList();

				var categoryViewModel = new CategoryViewModel
				{
					Id = item.Key,
					Name = productsInGroup.FirstOrDefault().CategoryName,
					ProductCount = productsInGroup.Count,
				};
				categoryViewModels.Add(categoryViewModel);
			
			}
			return categoryViewModels;
		}
	}
}

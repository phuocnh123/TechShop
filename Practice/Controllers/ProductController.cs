using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice.Controllers
{
    public class ProductController : Controller
    {

        private readonly List<LearnCategory> _categories;
        private readonly List<LearnProduct> _products;
        public ProductController()
        {
            _categories = new List<LearnCategory>()
            {
                new LearnCategory(1,"mobile"),
                new LearnCategory(2,"laptop"),
            };
            _products = new List<LearnProduct>()
            {
                new LearnProduct(1,"Iphone 6", 1000, 1),
                new LearnProduct(1,"Iphone 8", 1300, 1),
                new LearnProduct(1,"Iphone 5", 900, 1),
                new LearnProduct(1,"Samsung s10", 1100, 1),
                new LearnProduct(1,"xiaomi m1", 200, 1),

                new LearnProduct(1,"dell latitude", 2000, 2),
                new LearnProduct(1,"lenovo x1", 1700, 2),
                new LearnProduct(1,"hp zbook", 2200, 2),
                new LearnProduct(1,"dell precion", 3000, 2),
                new LearnProduct(1,"dell vostro", 1000, 2),
            };
        }
        public IActionResult Index()
        {
            return View(_categories);
        }

        public IActionResult ProductPartial([FromBody] FilterProductModel model)
        {
            var products = SearchProduct(model);
            return PartialView(products);
        }
        private List<LearnProductViewModel> SearchProduct(FilterProductModel model)
        {
            var result = from p in _products
                         join c in _categories
                         on p.CategoryId equals c.Id
                         select new LearnProductViewModel
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Price = p.Price,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name
                         };
            if (!string.IsNullOrEmpty(model.KeyWord))
            {
                result = result.Where(s => s.Name.Contains(model.KeyWord, StringComparison.OrdinalIgnoreCase));
            }
            if (model.CategoryId.HasValue)
            {
                result = result.Where(s => s.CategoryId == model.CategoryId);
            }

            result = result.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize);
            return result.ToList();
        }
    }
}

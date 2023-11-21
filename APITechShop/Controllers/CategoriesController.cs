using Infrastructure.Categories;
using Infrastructure.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APITechShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get-categories")]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetCategories();
            return Ok(result);
        }
        [HttpPost]
        [Route("search-products")]
        public IActionResult SearchProducts([FromBody] ProductFilterModel model)
        {
            var products = _productService.SearchProduct(model);
            return Ok(products);
        }

        [HttpPost]
        [Route("count-product")]
        public IActionResult CountProduct([FromBody] ProductFilterModel model)
        {
            var count = _productService.CountProduct(model);
            return Ok(count);
        }
        // api lay product detail
    }
}

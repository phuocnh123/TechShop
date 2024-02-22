using Infrastructure.Bills;
using Infrastructure.Categories;
using Infrastructure.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using Admin.Controllers; 


namespace Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        public CategoryController(IWebHostEnvironment webHostEnvironment, ICategoryService categoryService)
        {
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categoryViewModels = _categoryService.GetCategories();
            return View(categoryViewModels);
        }

        public IActionResult RemoveCategory(Guid categoryId)
        {
            _categoryService.RemoveCategory(categoryId);
            return RedirectToAction("Index", "Category");
        }

    }
}

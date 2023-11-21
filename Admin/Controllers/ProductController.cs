using Infrastructure.Categories;
using Infrastructure.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private const string ImageFolder = "product-images";

        public ProductController(IWebHostEnvironment webHostEnvironment, IProductService productService, ICategoryService categoryService)
        {
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _categoryService = categoryService;
        }
        [Authorize (Roles ="Admin")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            var categories = _categoryService.GetCategories();
            return View(categories);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ImageUrls = await SaveImage(model.Images);
            var response = await _productService.CreateProduct(model);
            TempData["response"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Index", "Product");

        }

        public IActionResult GetPagination_Pta([FromBody] ProductFilterModel model)
        {
            var products = _productService.SearchProduct(model);
            return PartialView(products);
        }

        public IActionResult CountPagination([FromBody] ProductFilterModel model)
        {
            var count = _productService.CountProduct(model);
            return Json(new { result = count });
        }


        private async Task<List<string>> SaveImage(List<IFormFile> images)
        {
            var imageLinks = new List<string>();
            foreach (var image in images)
            {
                string sWebRootFolder = _webHostEnvironment.WebRootPath;
                string directory = Path.Combine(sWebRootFolder, ImageFolder);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string fileName = $"{Guid.NewGuid()}-{image.FileName}";
                string fileUrl = $"{Request.Scheme}://{Request.Host}/{ImageFolder}/{fileName}";
                using var stream = new FileStream(Path.Combine(directory, fileName), FileMode.Create);
                await image.CopyToAsync(stream);
                imageLinks.Add(fileUrl);
            }
            return imageLinks;
        }
    }
}

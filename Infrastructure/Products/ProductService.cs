using Infrastructure.Commons;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Infrastructure.Products
{
    public interface IProductService
    {
        List<ProductViewModel> SearchProduct(ProductFilterModel filter);
        ProductDetailViewModel GetProductDetail(Guid productId);
        Task<ResponseResult> CreateProduct(ProductCreateViewModel model);
        int CountProduct(ProductFilterModel filter);

    }
    public class ProductService : IProductService
    {
        private readonly TechShopDbContext _context;

        public ProductService(TechShopDbContext context)
        {
            _context = context;
        }

        public ProductDetailViewModel GetProductDetail(Guid productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return null;
            }
            var result = new ProductDetailViewModel();
            result.Id = product.Id;
            result.Name = product.Name;
            result.Description = product.Description;
            result.Quantity = product.Quantity;
            result.Price = product.Price;
            result.DiscountPrice = product.DiscountPrice;
            result.CategoryId = product.CategoryId;
            result.CategoryName = GetCategory(product.CategoryId);
            result.Reviews = GetReviewModels(productId);
            result.Images = GetImageModels(productId);
            return result;
        }

        private string GetCategory(Guid categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            return category != null ? category.Name : string.Empty;
        }

        private List<ReviewModel> GetReviewModels(Guid productId)
        {
            var result = _context.Reviews
                .Where(s => s.ProductId == productId)
                .Select(x => new ReviewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    ReviewerName = x.ReviewerName,
                    Email = x.Email,
                    Rating = x.Rating

                });
            return result.ToList();
        }

        private List<ImageViewModel> GetImageModels(Guid productId)
        {

            var result = _context.ProductImages
                .Where(s => s.ProductId == productId)
                .Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    ImageLink = x.ImageLink,
                    Alt = x.Alt,
                });
            return result.ToList();
        }
        public int CountProduct(ProductFilterModel filter)
        {
            var result = BuildProductQuery(filter);
            return result.Count();
        }
        public List<ProductViewModel> SearchProduct(ProductFilterModel filter)
        {
            var productViewModels = new List<ProductViewModel>();
            var images = _context.ProductImages.AsQueryable();
            var reviews = _context.Reviews.AsQueryable();
            var result = BuildProductQuery(filter);
            productViewModels = result.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            foreach (var item in productViewModels)
            {
                var image = images.FirstOrDefault(s => s.ProductId == item.Id)?.ImageLink;
                item.Image = string.IsNullOrEmpty(image) ? string.Empty : image;

                var productReviews = reviews.Where(s => s.ProductId == item.Id);
                if (productReviews.Any())
                {
                    item.Rating = productReviews.Max(s => s.Rating);
                }
            }
            return productViewModels;
        }

        private IEnumerable<ProductViewModel> BuildProductQuery(ProductFilterModel filter)
        {
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
            if (!string.IsNullOrEmpty(filter.CategoryId) && Guid.TryParse(filter.CategoryId, out Guid categoryId))
            {
                result = result.Where(s => s.CategoryId == categoryId);
            }

            if (filter.FromPrice.HasValue && filter.ToPrice.HasValue)
            {
                result = result.Where(s => s.Price >= filter.FromPrice.Value && s.Price <= filter.ToPrice);
            }
            if (filter.ToPrice.HasValue && !filter.FromPrice.HasValue)
            {
                result = result.Where(s => s.Price <= filter.ToPrice.Value);
            }
            if (filter.FromPrice.HasValue && !filter.ToPrice.HasValue)
            {
                result = result.Where(s => s.Price >= filter.FromPrice.Value);
            }

            if (!string.IsNullOrEmpty(filter.KeyWord))
            {
                result = result.Where(s => s.Name.Contains(filter.KeyWord, StringComparison.OrdinalIgnoreCase) || s.CategoryName.Contains(filter.KeyWord, StringComparison.OrdinalIgnoreCase));
            }
            if (filter.SortBy.Equals(SortEnum.Price))
            {
                result = result.OrderBy(s => s.Price);
            }
            else
            {
                result = result.OrderBy(s => s.Name);
            }
            return result;
        }


        public async Task<ResponseResult> CreateProduct(ProductCreateViewModel model)
        {
            using var transcation = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = new Product
                {
                    Name = model.ProductName,
                    Description = model.Description,
                    Detail = model.Detail,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    Quantity = model.Quantity,
                    CategoryId = model.CategoryId,
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Status = Enums.EntityStatus.Active,
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                foreach (var item in model.ImageUrls)
                {
                    var image = new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ImageLink = item,
                        CreatedDate = product.CreatedDate,
                        Status = Enums.EntityStatus.Active,
                        ProductId = product.Id,
                        Alt = product.Name

                    };
                    _context.ProductImages.Add(image);
                    await _context.SaveChangesAsync();
                }
                await transcation.CommitAsync();
                return new ResponseResult(200, "Create product successfully");
            }
            catch (Exception)
            {
                await transcation.RollbackAsync();
                return new ResponseResult(400, "Some thing went wrong");
            }

        }
    }
}

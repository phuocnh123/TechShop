using Infrastructure.Commons;
using Infrastructure.Products;
using Microsoft.AspNetCore.Mvc;
using TechShop.Constants;
using TechShop.Extentions;
using TechShop.Models;

namespace TechShop.Controllers
{
    public class CartController : Controller
	{
		private readonly IProductService _productService;

		public CartController(IProductService productService)
		{
			_productService = productService;
		}
		public IActionResult AddToCart(Guid productId)
		{
			var product = _productService.GetProductDetail(productId);
			if (product == null)
			{
				return Json(new ResponseResult(404, "Product is not found"));
			}
			var cartItem = new CartItemViewModel
			{
				ProductName = product.Name,
				ProductId = product.Id,
				Quantity = 1,
				Image = product.Images?.FirstOrDefault()?.ImageLink,
				Price = product.DiscountPrice.HasValue ? product.DiscountPrice.Value : product.Price,
			};
			var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
			if (cart == null)
			{
				HttpContext.Session.SetCart(TechShopConstant.Cart, new List<CartItemViewModel>() { cartItem });
			}
			else
			{
				UpdateCartItemQuantity(cart, cartItem);
				HttpContext.Session.SetCart(TechShopConstant.Cart, cart);
			}
			return Json(new ResponseResult(200, $"Add {cartItem.ProductName} to cart success!"));
		}

		public IActionResult RemoveFromCart(Guid productId)
		{
			var product = _productService.GetProductDetail(productId);
			if (product == null)
			{
				return Json(new ResponseResult(404, "Product is not found"));
			}
			var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
			if (cart == null)
			{
				return Json(new ResponseResult(404, "Cart is empty"));
			}
			else
			{
				cart.RemoveAll(s=> s.ProductId == productId);
				HttpContext.Session.SetCart(TechShopConstant.Cart, cart);
				return Json(new ResponseResult(200, $"Remove {product.Name} success!"));
			}
		}

		public IActionResult CartPartial()
		{
			var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
			return PartialView(cart);
		}

		private void UpdateCartItemQuantity(List<CartItemViewModel> cart, CartItemViewModel cartItem)
		{
			var item = cart.FirstOrDefault(s => s.ProductId == cartItem.ProductId);
			if (item != null)
			{
				item.Quantity += 1;
			}
			else
			{
				cart.Add(cartItem);
			}
		}
	}
}

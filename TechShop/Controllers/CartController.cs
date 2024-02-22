using Infrastructure.Bills;
using Infrastructure.Commons;
using Infrastructure.Enums;
using Infrastructure.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechShop.Constants;
using TechShop.Extentions;
using TechShop.Helpers;
using TechShop.Models;
using TechShop.Services;

namespace TechShop.Controllers
{
    public class CartController : Controller
	{
		private readonly IBillService _billService;
		private readonly IProductService _productService;
        private readonly IVnPayService _vnPayservice;
        public CartController(IBillService billService, IProductService productService, IVnPayService vnPayservice)
		{
			_billService = billService;
			_productService = productService;
            _vnPayservice = vnPayservice;
        }

		#region test

		public IActionResult Index()
		{
			var model = new CheckoutViewModel();
			var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
			model.Items = cart;
			model.ShippingMethod = EnumHelper.GetList(typeof(PaymentMethod));
			return View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> PlaceOrder(CustomerInfoModel model) // check lại sau9
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
			if (cart == null)
			{
				return Json(new ResponseResult(400, "cart is empty"));
			}
			BillCreateViewModel billModel = new BillCreateViewModel();
			billModel.FirstName = model.FirstName;
			billModel.LastName = model.LastName;
			billModel.Email = model.Email;
			billModel.PhoneNumber = model.PhoneNumber;
			billModel.Address = model.Address;
			billModel.PaymentMethod = model.PaymentMethod;
			billModel.BillDetails = cart.Select(s => new BillDetailCreateViewModel
			{
				Price = s.Price,
				ProductName = s.ProductName,
				Quantity = s.Quantity,
			}).ToList();
			var response = await _billService.CreateBill(billModel);
			HttpContext.Session.Remove(TechShopConstant.Cart);
			TempData["checkout"] = JsonConvert.SerializeObject(response);
			if (model.PaymentMethod.ToString() == "VnPay")
			{

				// Đoạn code xử lý khi PaymentMethod là VnPay
				var vnPayModel = new VnPaymentRequestModel
				{
					Amount = (double)billModel.BillDetails.Sum(s => s.Quantity * s.Price),
					CreatedDate = DateTime.Now,
					Description = $"{model.FirstName + model.LastName} - {model.PhoneNumber}",
					FullName = model.FirstName + model.LastName,
					OrderId = new Random().Next(1000, 100000)
				};
				return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
			}
			return RedirectToAction("Index", "Checkout");
		}
		public IActionResult PaymentCallBack(string? url)
		{
			var response = _vnPayservice.PaymentExecute(Request.Query);

			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}


			// Lưu đơn hàng vô database

			TempData["Message"] = $"Thanh toán VNPay thành công";
			return RedirectToAction("PaymentSuccess");
		}

		#endregion

		public IActionResult AddToCart(Guid productId)
		{
			var product = _productService.GetProductDetail(productId);
			if (product == null)
			{
				return Json(new ResponseResult(404, "Product is sold out"));
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

        [Authorize]
        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }
        [Authorize]
        public IActionResult PaymentFail()
        {
            return View();
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

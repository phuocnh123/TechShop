using Infrastructure.Bills;
using Infrastructure.Commons;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TechShop.Constants;
using TechShop.Extentions;
using TechShop.Helpers;
using TechShop.Models;
using TechShop.Services;
using Microsoft.AspNetCore.Authorization;

namespace TechShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IBillService _billService;
        private readonly IVnPayService _vnPayservice;
        public CheckoutController(IBillService billService, IVnPayService vnPayservice)
        {
            _billService = billService;
            _vnPayservice = vnPayservice;
        }
  //      public IActionResult Index()
  //      {
  //          var model = new CheckoutViewModel();
  //          var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
  //          model.Items = cart;
  //          model.ShippingMethod = EnumHelper.GetList(typeof(PaymentMethod));
  //          return View(model);
  //      }

  //      [ValidateAntiForgeryToken]
  //      [HttpPost]
  //      public async Task<IActionResult> PlaceOrder(CustomerInfoModel model) // check lại sau9
  //      {
  //          if (!ModelState.IsValid)
  //          {
  //              return View(model);
  //          }
            
  //          var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
  //          if (cart == null)
  //          {
  //              return Json(new ResponseResult(400, "cart is empty"));
  //          }
  //          BillCreateViewModel billModel = new BillCreateViewModel();
  //          billModel.FirstName = model.FirstName;
  //          billModel.LastName = model.LastName;
  //          billModel.Email = model.Email;
  //          billModel.PhoneNumber = model.PhoneNumber;
  //          billModel.Address = model.Address;
  //          billModel.PaymentMethod = model.PaymentMethod;
  //          billModel.BillDetails = cart.Select(s => new BillDetailCreateViewModel
  //          {
  //              Price = s.Price,
  //              ProductName = s.ProductName,
  //              Quantity = s.Quantity,
  //          }).ToList();
  //          var response = await _billService.CreateBill(billModel);
  //          HttpContext.Session.Remove(TechShopConstant.Cart);
  //          TempData["checkout"] = JsonConvert.SerializeObject(response);
  //          if (model.PaymentMethod.ToString() == "VnPay")
  //          {

  //              // Đoạn code xử lý khi PaymentMethod là VnPay
  //              var vnPayModel = new VnPaymentRequestModel
  //              {
  //                  Amount = (double)billModel.BillDetails.Sum(s => s.Quantity * s.Price),
  //                  CreatedDate = DateTime.Now,
  //                  Description = $"{model.FirstName + model.LastName} - {model.PhoneNumber}",
  //                  FullName = model.FirstName + model.LastName,
  //                  OrderId = new Random().Next(1000, 100000)
  //              };
  //              return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
  //          }
  //          return RedirectToAction("Index", "Checkout");
  //      }
		//public IActionResult PaymentCallBack(string? url)
		//{
		//	var response = _vnPayservice.PaymentExecute(Request.Query);

		//	if (response == null || response.VnPayResponseCode != "00")
		//	{
		//		TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
		//		return RedirectToAction("PaymentFail");
		//	}


		//	// Lưu đơn hàng vô database

		//	TempData["Message"] = $"Thanh toán VNPay thành công";
		//	return RedirectToAction("PaymentSuccess");
		//}
	}
}

using Infrastructure.Bills;
using Infrastructure.Categories;
using Infrastructure.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BillController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBillService _billService;

        public BillController(IWebHostEnvironment webHostEnvironment, IBillService billService)
        {
            _webHostEnvironment = webHostEnvironment;
            _billService = billService;
        }

        public IActionResult Index()
        {
            var bills = _billService.GetBills();

            return View(bills);
        }
    }
}

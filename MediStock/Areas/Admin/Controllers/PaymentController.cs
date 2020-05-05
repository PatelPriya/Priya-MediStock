using MediStockWeb.Areas.Admin.Controllers.Base;
using MediStockWeb.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediStockWeb.Areas.Admin.Controllers
{
    public class PaymentController : BaseController
    {
        public IActionResult Index()
        {
            var model = new PaymentModel();
            TempData["PaymentActiveMenuItem"] = "payment-sidebar-id";
            return View(model);
        }
        
        public IActionResult AddCard()
        {
            var model = new PaymentCardModel();
            TempData["PaymentActiveMenuItem"] = "payment-sidebar-id";
            return View(model);
        }
        
        [HttpPost]
        public IActionResult AddCard(PaymentCardModel model)
        {            
            return View(model);
        }
        [HttpPost]
        public ActionResult paytmResponse(PaytmResponseModel data)
        {
            return View("paytmResponse", data);
        }
    }
}
using MediStockWeb.Controllers.Base;
using MediStockWeb.Models;
using MediStockWeb.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediStockWeb.Controllers
{
    public class AuthenticationController : BaseController
    {
       
        public IActionResult Auth()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SignUp(CustomerModel model)
        {
            return View();
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> SignIn(string returnUrl = null)
        //{
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SignIn(CustomerModel model, string returnUrl = null)
        //{
        //}

        [HttpPost]
        public IActionResult SignIn(CustomerModel model)
        {
            return RedirectToAction(nameof(Auth));
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ForgotPassword(AuthenticationModel model)
        {
            return View();
        }
    }
}
using DAL.Domains;
using MediStockWeb.Areas.Admin.Models;
using MediStockWeb.Models;
using MediStockWeb.Models.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using paytm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediStockWeb.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            var model = new PaymentRequestDataModel();
            return View(model);

        }
        [HttpPost]
        public ActionResult CreatePayment(PaymentRequestDataModel requestData, OrderModel orderModel, CustomerModel customerModel)
        {
            String merchantKey = Key.merchantKey;

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            
            parameters.Add("MID", Key.merchantId);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", requestData.Email);
            parameters.Add("MOBILE_NO", requestData.MobileNumber);
            //parameters.Add("CUST_ID", customerModel.CustomerId);
            parameters.Add("CUST_ID", "1");
            //parameters.Add("ORDER_ID", orderModel.OrderId);
             parameters.Add("ORDER_ID", "ORD879631"); //OrderId must be unique everytime.
            parameters.Add("TXN_AMOUNT", requestData.Amount);
            parameters.Add("CALLBACK_URL", "https://localhost:44388/Payment/PaytmResponse");
           // parameters.Add("CALLBACK_URL", "url");//This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);

            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";

            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }

            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;
            

            return View("PaymentPage");
        }
        [HttpPost]
        public ActionResult PaytmResponse(PaytmResponseModel data)
        {
            return View(data);
        }
        
        public ActionResult Success()
        {
            return View("Success");
        }


    }
}

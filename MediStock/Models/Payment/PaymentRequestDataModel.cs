using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediStockWeb.Models.Payment
{
    public class PaymentRequestDataModel
    {
        public int PaymentId { get; set; }
        public string Amount { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}

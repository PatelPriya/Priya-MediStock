using DAL.Domains.Base;
using DAL.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domains
{
   public partial class PaymentRequestData : BaseEntity
    {
        public string Amount { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        //public ICollection<PaymentOrder> _paymentOrder;

        //public virtual ICollection<PaymentOrder> PaymentOrders
        //{
        //    get => _paymentOrder ?? (_paymentOrder = new List<PaymentOrder>());
        //    set => _paymentOrder = value;
        //}

        //public ICollection<CustomerPayment> _customerPayment;

        //public virtual ICollection<CustomerPayment> CustomerPayments
        //{
        //    get => _customerPayment ?? (_customerPayment = new List<CustomerPayment>());
        //    set => _customerPayment = value;
        //}
    }
}

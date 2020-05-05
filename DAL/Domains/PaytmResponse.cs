using DAL.Domains.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Domains
{
   public partial class PaytmResponse : BaseEntity
    {
        public string MId { get; set; }
        public string OrderId { get; set; }
        public string TxnAmount { get; set; }
        public string Currency { get; set; }
        public string TxnId { get; set; }
        public string Status { get; set; }
        public string RespCode { get; set; }
        public string RespMsg { get; set; }
        public string TxnDate { get; set; }
        public string GatewayName { get; set; }
        public string BankName { get; set; }
        public string PaymentMode { get; set; }
        public string Checksumhash { get; set; }
    }
}

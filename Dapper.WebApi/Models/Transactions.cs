using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dapper.WebApi.Models
{
    [XmlRoot("Transactions")]
    public class Transactions
    {
        [XmlElement("Transaction")]
        public List<Transaction> Transaction { get; set; }
        
    }

    
    public class Transaction
    {
        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlElement("TransactionDate")]
        public DateTime TransactionDate { get; set; }
        [XmlElement("Status")]
        public string Status { get; set; }
        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }
    }

    public class PaymentDetails
    {
        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
        [XmlElement("Amount")]
        public decimal Amount { get; set; }
    }
}

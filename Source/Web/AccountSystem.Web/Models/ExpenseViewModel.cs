namespace AccountSystem.Web.Models
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using AccountSystem.Models;

    public class ExpenseViewModel
    {
        public int Id { get; set; }

        public string PayerName { get; set; }

        public IEnumerable<SelectListItem> Payers { get; set; }

        public string CustomerName { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        public string ShopName { get; set; }

        public IEnumerable<SelectListItem> ShopNames { get; set; }

        public string WorkName { get; set; }

        public IEnumerable<SelectListItem> WorkNames { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ReceiptNumber { get; set; }

        public decimal Amount { get; set; }

        public decimal PrivateAmount { get; set; }
    }
}
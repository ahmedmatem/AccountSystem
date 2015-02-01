namespace AccountSystem.Web.Models
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountSystem.Models;

    public class ExpenseViewModel
    {
        public int Id { get; set; }
        
        public string PayerName { get; set; }

        public IEnumerable<SelectListItem> Payers { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        [Required]
        public string ShopName { get; set; }

        public IEnumerable<SelectListItem> ShopNames { get; set; }

        [Required]
        public string WorkName { get; set; }

        public IEnumerable<SelectListItem> WorkNames { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string ReceiptNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal PrivateAmount { get; set; }

        public bool IsCreditCardPayment { get; set; }

        [Required]
        [Display(Name = "Credit card")]
        public int CreditCardId { get; set; }
    }
}
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

        public int ParentId { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Author { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string PayerName { get; set; }

        public IEnumerable<SelectListItem> Payers { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        [Required]
        public string ShopName { get; set; }

        public IEnumerable<SelectListItem> Shops { get; set; }

        [Required]
        public string WorkName { get; set; }

        public IEnumerable<SelectListItem> Works { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string ReceiptNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal PrivateAmount { get; set; }

        public bool IsCreditCardPayment { get; set; }

        [Display(Name = "Credit card")]
        public int CreditCardId { get; set; }

        public virtual IEnumerable<SelectListItem> CreditCards { get; set; }

        public string CreditCardName { get; set; }

        public string TextColor { get; set; }
    }
}
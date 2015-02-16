namespace AccountSystem.Web.Models
{
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountSystem.Models;

    public class RevenueViewModel
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Author { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Recipient name")]
        public string RecipientName { get; set; }

        [Required(ErrorMessage="\"Recipient name\" field is required.")]
        public string RecipientId { get; set; }

        public IEnumerable<SelectListItem> Recipients { get; set; }

        [Display(Name="Customer name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "\"Customer name\" field is required.")]
        public int CustomerId { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        [Display(Name="Work name")]
        public string WorkName { get; set; }

        [Required(ErrorMessage = "\"Work name\" field is required.")]
        public int WorkId { get; set; }

        public IEnumerable<SelectListItem> Works { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public bool IsCreditCardPayment { get; set; }

        [Display(Name = "Credit card")]
        public int CreditCardId { get; set; }

        public virtual IEnumerable<SelectListItem> CreditCards { get; set; }

        public string CreditCardName { get; set; }

        public bool HasInvoice { get; set; }

        public string TextColor { get; set; }

        public string Description { get; set; }
    }
}
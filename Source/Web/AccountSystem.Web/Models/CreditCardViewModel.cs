namespace AccountSystem.Web.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.WebPages.Html;
    using System.ComponentModel.DataAnnotations;

    using AccountSystem.Models;

    public class CreditCardViewModel
    {
        public CreditCardViewModel()
        {
            //this.CreditCardType = new CreditCardType();
        }

        public int CreditCardId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Credit card")]
        public CreditCardType BankName { get; set; }

        //[Required]
        //public CreditCardType CreditCardType { get; set; }

        [Required]
        [Display(Name="Card number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Valid thru (dd/yy)")]
        public string ValidThru { get; set; }
    }
}
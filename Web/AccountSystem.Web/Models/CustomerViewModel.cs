namespace AccountSystem.Web.Models
{
    using System;
    using System.Web.WebPages.Html;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountSystem.Models;

    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // public DateTime CreatedOn { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        [Display(Name = "Post code")]
        public string PostCode { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Other details")]
        public string OtherDetails { get; set; }
    }
}
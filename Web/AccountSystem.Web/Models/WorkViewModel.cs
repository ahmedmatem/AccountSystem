namespace AccountSystem.Web.Models
{
    using System;
    using System.Web.Mvc;

    using AccountSystem.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class WorkViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Work name")]
        public string Name { get; set; }

        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Other details")]
        public string OtherDetails { get; set; }

        public Customer Customer { get; set; }
    }
}
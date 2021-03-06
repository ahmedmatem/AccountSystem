﻿namespace AccountSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public CreditCardType BankName { get; set; }

        public string CardNumber { get; set; }

        public string ValidThru { get; set; }

        public string AccountHolderId { get; set; }

        public virtual ApplicationUser AccountHolder { get; set; }

        public bool IsActive { get; set; }
    }
}

namespace AccountSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Expense
    {
        public Expense() { }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ReceiptNumber { get; set; }

        // Total expense amount
        public decimal Amount { get; set; }

        // PrivateAmount is a part of expense Amount for personal use
        public decimal PrivateAmount { get; set; }

        public string PayerId { get; set; }

        public int CustomerId { get; set; }

        public int ShopId { get; set; }

        public int CreditCardId { get; set; }

        public bool IsCreditCardPayment { get; set; }

        public virtual ApplicationUser Payer { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Shop Shop { get; set; }
    }
}

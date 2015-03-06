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

        public int ParentId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ActionType Action { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string ReceiptNumber { get; set; }

        // Total expense amount
        public decimal Amount { get; set; }

        // SharedAmount is amount of the expense made from both of them (50%50)
        public decimal SharedAmount { get; set; }

        // PrivateAmount is a part of expense Amount for personal use
        public decimal PrivateAmount { get; set; }

        public string PayerId { get; set; }

        public int CustomerId { get; set; }

        public int WorkId { get; set; }

        public int ShopId { get; set; }

        public int CreditCardId { get; set; }

        public bool IsCreditCardPayment { get; set; }

        public virtual ApplicationUser Payer { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Shop Shop { get; set; }

        public string TextColor { get; set; }
    }
}

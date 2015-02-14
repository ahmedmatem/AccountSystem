namespace AccountSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Revenue
    {
        public Revenue() { }

        [Key]
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ActionType Action { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        //public string ReceiptNumber { get; set; }

        // Total expense amount
        public decimal Amount { get; set; }

        // PrivateAmount is a part of expense Amount for personal use
        //public decimal PrivateAmount { get; set; }

        public string RecipientId { get; set; }

        public int CustomerId { get; set; }

        public int WorkId { get; set; }

        //public int ShopId { get; set; }

        public int CreditCardId { get; set; }

        public bool IsCreditCardPayment { get; set; }

        public virtual ApplicationUser Recipient { get; set; }

        public virtual Customer Customer { get; set; }

        //public virtual Shop Shop { get; set; }

        public bool HasInvoice { get; set; }

        public string TextColor { get; set; }
    }
}

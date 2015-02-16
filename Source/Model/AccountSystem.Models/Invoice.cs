namespace AccountSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int InvoiceNumber { get; set; }

        public int RevenueId { get; set; }

        public string UserId { get; set; }
    }
}

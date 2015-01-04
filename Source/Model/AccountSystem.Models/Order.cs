using System.ComponentModel.DataAnnotations;
namespace AccountSystem.Models
{
    using System;

    public class Order
    {
        public Order() { }

        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

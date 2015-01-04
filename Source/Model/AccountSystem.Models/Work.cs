namespace AccountSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Work
    {
        public Work() { }

        [Key]
        public int Id { get; set; }

        public ICollection<WorkType> Types { get; set; }

        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}

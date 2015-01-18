namespace AccountSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Work
    {
        public Work() { }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        //public ICollection<WorkType> Types { get; set; }

        public decimal Price { get; set; }

        public string OtherDetails { get; set; }

        public bool IsActive { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}

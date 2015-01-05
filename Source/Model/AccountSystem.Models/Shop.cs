namespace AccountSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        [Key]
        public int Id { get; set; }

        public Shop() { }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}

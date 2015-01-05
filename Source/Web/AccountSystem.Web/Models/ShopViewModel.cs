namespace AccountSystem.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShopViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
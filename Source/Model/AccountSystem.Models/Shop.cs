namespace AccountSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Shop
    {
        protected ICollection<Expense> expenses;

        public Shop()
        {
            this.expenses = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Expense> Expenses
        {
            get
            {
                return this.expenses;
            }

            private set
            {
                this.expenses = value;
            }
        }
    }
}

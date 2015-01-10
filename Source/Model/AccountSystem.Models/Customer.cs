namespace AccountSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        protected ICollection<Order> orders;
        protected ICollection<Work> works;
        protected ICollection<Expense> expenses;

        public Customer()
        {
            this.orders = new HashSet<Order>();
            this.works = new HashSet<Work>();
            this.expenses = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string PhoneNumber { get; set; }

        public string OtherDetails { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders
        {
            get
            {
                return this.orders;
            }

            private set
            {
                this.orders = value;
            }
        }

        public virtual ICollection<Work> Works
        {
            get
            {
                return this.works;
            }

            private set
            {
                this.works = value;
            }
        }

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

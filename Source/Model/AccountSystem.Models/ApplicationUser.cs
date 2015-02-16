namespace AccountSystem.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        protected ICollection<Expense> expenses;
        protected ICollection<Revenue> revenues;
        protected ICollection<CreditCard> creditCards;

        public ApplicationUser()
        {
            this.expenses = new HashSet<Expense>();
            this.revenues = new HashSet<Revenue>();
            this.creditCards = new HashSet<CreditCard>();
        }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string CompanyName { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Fax { get; set; }

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

        public virtual ICollection<Revenue> Revenues
        {
            get
            {
                return this.revenues;
            }

            private set
            {
                this.revenues = value;
            }
        }

        public virtual ICollection<CreditCard> CrediCards
        {
            get
            {
                return this.creditCards;
            }

            private set
            {
                this.creditCards = value;
            }
        }

    }
}

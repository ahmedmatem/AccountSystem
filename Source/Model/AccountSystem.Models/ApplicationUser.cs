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

namespace AccountSystem.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        protected ICollection<Expense> expenses;
        protected ICollection<CreditCard> crediCards;

        public ApplicationUser()
        {
            this.expenses = new HashSet<Expense>();
            this.crediCards = new HashSet<CreditCard>();
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

        public virtual ICollection<CreditCard> CrediCards
        {
            get
            {
                return this.crediCards;
            }

            private set
            {
                this.crediCards = value;
            }
        }

    }
}

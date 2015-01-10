namespace AccountSystem.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        protected ICollection<Expense> expenses;

        public ApplicationUser()
        {
            this.expenses = new HashSet<Expense>();
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

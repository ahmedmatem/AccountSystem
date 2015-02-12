namespace AccountSystem.Data
{
    using AccountSystem.Data.Migrations;
    using AccountSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public IDbSet<Shop> Shops { get; set; }

        public IDbSet<Work> Works { get; set; }

        public IDbSet<Expense> Expenses { get; set; }


        public IDbSet<Revenue> Revenues { get; set; }

        public IDbSet<CreditCard> CreditCards { get; set; }
    }
}

namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AccountSystem.Web.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Web.Security;

    public class ExpensesController : BaseController
    {
        //
        // GET: /Expenses/
        public ActionResult Index()
        {
            var expenses = this.context.Expenses
                .Select(e => new ExpenseViewModel()
                {
                    PayerName = e.Payer.UserName,
                    CustomerName = e.Customer.Name,
                    ShopName = e.Shop.Name,
                    CreatedOn = e.CreatedOn,
                    ReceiptNumber = e.ReceiptNumber,
                    Amount = e.Amount,
                    PrivateAmount = e.PrivateAmount,
                }).ToList();

            return View(expenses);
        }

        //
        // GET: /Expenses/Create
        public ActionResult Create()
        {
            var model = new ExpenseViewModel()
            {
                Payers = this.context.Users
                .Where(u => u.Roles.Any(r => r.Role.Name == "AccountHolder"))
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.Id,
                    Selected = (u.UserName == User.Identity.Name),
                }).ToList(),
                Customers = this.context.Customers
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }).ToList(),
                ShopNames = this.context.Shops
                .Where(s => s.IsActive)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                }).ToList(),
            };

            return View(model);
        }
	}
}
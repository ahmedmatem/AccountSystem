namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.Mvc;

    using AccountSystem.Web.Models;
    using Microsoft.AspNet.Identity;
    using AccountSystem.Models;

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
                    IsCreditCardPayment = e.IsCreditCardPayment
                }).ToList();

            return View(expenses);
        }

        //
        // GET: /Expenses/Create
        public ActionResult Create()
        {
            ViewBag.AccountHoldersCreditCards = this.context.CreditCards
                .Select(cc => new
                {
                    Text = cc.Name,
                    Value = cc.Id.ToString()
                }).ToList();

            var model = new ExpenseViewModel()
            {
                //PayerName = User.Identity.Name,
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
                CreatedOn = DateTime.Now,
            };

            return View(model);
        }

        // TODO: Make a Create POST action
        //
        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseViewModel model)
        {
            ViewBag.AccountHoldersCreditCards = this.context.CreditCards
                .Select(cc => new
                {
                    Text = cc.Name,
                    Value = cc.Id.ToString()
                }).ToList();

            model.PayerName = this.context.Users.Find(model.PayerName).UserName;

            if (ModelState.IsValid)
            {
                var expence = new Expense()
                {
                    CreatedOn = model.CreatedOn,
                    Payer = this.context.Users.First(u => u.UserName.Equals(model.PayerName)),
                    //PayerId = model.PayerName,
                    Customer = this.context.Customers.Find(int.Parse(model.CustomerName)),
                    //CustomerId = int.Parse(model.CustomerName),
                    Shop = this.context.Shops.Find(int.Parse(model.ShopName)),
                    //ShopId = int.Parse(model.ShopName),
                    ReceiptNumber = model.ReceiptNumber,
                    Amount = model.Amount,
                    PrivateAmount = model.PrivateAmount,
                    IsCreditCardPayment = model.IsCreditCardPayment,
                    CreditCardId = model.IsCreditCardPayment ? model.CreditCardId : 0
                };

                this.context.Expenses.Add(expence);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            model.Payers = this.context.Users
                .Where(u => u.Roles.Any(r => r.Role.Name == "AccountHolder"))
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.Id,
                    Selected = (u.UserName == User.Identity.Name),
                }).ToList();

            model.Customers = this.context.Customers
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }).ToList();

            model.ShopNames = this.context.Shops
                .Where(s => s.IsActive)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                }).ToList();

            return View(model);
        }

        //
        // AJAX: Expenses/GetCustomerWorks

        public JsonResult GetCustomerWorks(int id)
        {
            var customerWorks = this.context.Customers
                .Where(c => c.Id == id)
                .Select(c => c.Works);

            return Json(customerWorks, JsonRequestBehavior.AllowGet);
        }
	}
}
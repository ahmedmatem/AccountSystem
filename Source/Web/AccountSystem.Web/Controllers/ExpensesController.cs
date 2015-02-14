namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;

    public class ExpensesController : BaseController
    {
        private string textColor;

        //
        // GET: /Expenses/
        public ActionResult Index()
        {
            var expenses = this.context.Expenses
                .Where(e => e.IsActive && !e.IsDeleted)
                .Select(e => new ExpenseViewModel()
                {
                    Id = e.Id,
                    ParentId = e.ParentId,
                    PayerName = e.Payer.UserName,
                    Action = e.Action.ToString(),
                    CustomerName = e.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == e.WorkId).Name,
                    ShopName = e.Shop.Name,
                    CreatedOn = e.CreatedOn,
                    ReceiptNumber = e.ReceiptNumber,
                    Amount = e.Amount,
                    PrivateAmount = e.PrivateAmount,
                    IsCreditCardPayment = e.IsCreditCardPayment,
                    CreditCardName = this.context.CreditCards.FirstOrDefault(c => c.Id == e.CreditCardId).Name,
                    TextColor = e.TextColor,
                    Author = e.Author,
                    ModifiedOn = e.ModifiedOn,
                }).ToList();

            return View(expenses);
        }

        //
        // GET: /Expenses/ListDeleted
        public ActionResult ListDeleted()
        {
            var expenses = this.context.Expenses
                .Where(e => e.IsActive && e.IsDeleted)
                .Select(e => new ExpenseViewModel()
                {
                    Id = e.Id,
                    ParentId = e.ParentId,
                    PayerName = e.Payer.UserName,
                    Action = e.Action.ToString(),
                    CustomerName = e.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == e.WorkId).Name,
                    ShopName = e.Shop.Name,
                    CreatedOn = e.CreatedOn,
                    ReceiptNumber = e.ReceiptNumber,
                    Amount = e.Amount,
                    PrivateAmount = e.PrivateAmount,
                    IsCreditCardPayment = e.IsCreditCardPayment,
                    CreditCardName = this.context.CreditCards.FirstOrDefault(c => c.Id == e.CreditCardId).Name,
                    TextColor = e.TextColor,
                    Author = e.Author,
                    ModifiedOn = e.ModifiedOn,
                }).ToList();

            return View(expenses);
        }

        private void InitializeDropDownFields(ExpenseViewModel model)
        {
            model.Payers = this.context.Users
                .Where(u => u.Roles.Any(r => r.Role.Name == "AccountHolder"))
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.Id,
                    Selected = (u.UserName == model.PayerName),
                }).ToList();

            model.Customers = this.context.Customers
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = (c.Name == model.CustomerName),
                }).ToList();

            model.Works = this.context.Works
                .Where(w => w.IsActive)
                .Select(w => new SelectListItem()
                {
                    Text = w.Name,
                    Value = w.Id.ToString(),
                    Selected = (w.Name == model.WorkName),
                }).ToList();

            model.Shops = this.context.Shops
                .Where(s => s.IsActive)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                    Selected = (s.Name == model.ShopName),
                }).ToList();

            model.CreditCards = this.context.CreditCards
                .Select(cc => new SelectListItem
                {
                    Text = cc.Name,
                    Value = cc.Id.ToString(),
                    Selected = (cc.Id == model.CreditCardId),
                }).ToList();
        }

        //
        // GET: /Expenses/Create
        public ActionResult Create()
        {
            var model = new ExpenseViewModel()
            {
                CreatedOn = DateTime.Now,
            };

            this.InitializeDropDownFields(model);

            return View(model);
        }

        
        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseViewModel model)
        {
            model.Author = User.Identity.Name;

            if (model.Author.Equals("levend"))
            {
                this.textColor = ConfigurationManager.AppSettings["AccountHolderTextColor"];
            }

            if (ModelState.IsValid)
            {
                var expence = new Expense()
                {
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = DateTime.Now,
                    Payer = this.context.Users.Find(model.PayerName),
                    Author = model.Author,
                    Action = ActionType.Created,
                    IsActive = true,
                    Customer = this.context.Customers.Find(int.Parse(model.CustomerName)),
                    WorkId = int.Parse(model.WorkName),
                    Shop = this.context.Shops.Find(int.Parse(model.ShopName)),
                    ReceiptNumber = model.ReceiptNumber,
                    Amount = model.Amount,
                    PrivateAmount = model.PrivateAmount,
                    IsCreditCardPayment = model.IsCreditCardPayment,
                    CreditCardId = model.IsCreditCardPayment ? model.CreditCardId : 0,
                    TextColor = this.textColor,
                };

                this.context.Expenses.Add(expence);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            this.InitializeDropDownFields(model);

            return View(model);
        }

        //
        // GET: /Expenses/Update/id
        public ActionResult Update(int id)
        {
            var matchedExpense = this.context.Expenses.Find(id);

            var model = new ExpenseViewModel
            {
                Id = matchedExpense.Id,
                PayerName = matchedExpense.Payer.Id,
                CustomerName = matchedExpense.Customer.Id.ToString(),
                WorkName = matchedExpense.WorkId.ToString(),
                ShopName = matchedExpense.Shop.Id.ToString(),
                CreatedOn = matchedExpense.CreatedOn,
                ReceiptNumber = matchedExpense.ReceiptNumber,
                Amount = matchedExpense.Amount,
                PrivateAmount = matchedExpense.PrivateAmount,
                CreditCardId = matchedExpense.CreditCardId,
                IsCreditCardPayment = matchedExpense.IsCreditCardPayment,
            };

            this.InitializeDropDownFields(model);

            return View(model);
        }


        // POST: Expenses/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ExpenseViewModel model)
        {
            model.Author = User.Identity.Name;

            if (model.Author.Equals("levend"))
            {
                this.textColor = ConfigurationManager.AppSettings["AccountHolderTextColor"];
            }

            if (ModelState.IsValid)
            {
                var oldExpense = this.context.Expenses.Find(model.Id);
                oldExpense.IsActive = false;

                var expence = new Expense()
                {
                    ParentId = (oldExpense.ParentId == 0 ? oldExpense.Id : oldExpense.ParentId),
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = DateTime.Now,
                    Payer = this.context.Users.Find(model.PayerName),
                    Author = model.Author,
                    Action = ActionType.Modified,
                    IsActive = true,
                    Customer = this.context.Customers.Find(int.Parse(model.CustomerName)),
                    WorkId = int.Parse(model.WorkName),
                    Shop = this.context.Shops.Find(int.Parse(model.ShopName)),
                    ReceiptNumber = model.ReceiptNumber,
                    Amount = model.Amount,
                    PrivateAmount = model.PrivateAmount,
                    IsCreditCardPayment = model.IsCreditCardPayment,
                    CreditCardId = model.IsCreditCardPayment ? model.CreditCardId : 0,
                    TextColor = this.textColor,
                };

                this.context.Expenses.Add(expence);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            this.InitializeDropDownFields(model);

            return View(model);
        }

        //
        // AJAX: Expenses/GetCustomerWorks

        public JsonResult GetCustomerWorks(int id)
        {
            var customer = this.context.Customers.Find(id);

            var result = new List<SelectListItem>();
            foreach (var work in customer.Works)
            {
                result.Add(new SelectListItem()
                {
                    Text = work.Name,
                    Value = work.Id.ToString(),
                });
            }

            //return Json(customerWorks, JsonRequestBehavior.AllowGet);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: Expenses/Delete/id

        public ActionResult Delete(int id)
        {
            var oldExpense = this.context.Expenses.Find(id);
            oldExpense.IsActive = false;

            var deletedExpense = new Expense()
            {
                ParentId = (oldExpense.ParentId == 0 ? oldExpense.Id : oldExpense.ParentId),
                PayerId = oldExpense.PayerId,
                Author = User.Identity.Name,
                CreatedOn = oldExpense.CreatedOn,
                ModifiedOn = DateTime.Now,
                Action = ActionType.Deleted,
                IsActive = true,
                IsDeleted = true,
                ReceiptNumber = oldExpense.ReceiptNumber,
                Amount = oldExpense.Amount,
                PrivateAmount = oldExpense.PrivateAmount,
                CustomerId = oldExpense.CustomerId,
                ShopId = oldExpense.ShopId,
                CreditCardId = oldExpense.CreditCardId,
                IsCreditCardPayment = oldExpense.IsCreditCardPayment,
                TextColor = oldExpense.TextColor,
                WorkId = oldExpense.WorkId,
            };

            this.context.Expenses.Add(deletedExpense);

            this.context.SaveChanges();

            return RedirectToAction("index");
        }

        //
        // GET: Expenses/Restore/id

        public ActionResult Restore(int id)
        {
            var matchedExpense = this.context.Expenses.Find(id);
            matchedExpense.IsActive = false;

            var restoredExpense = new Expense()
            {
                ParentId = (matchedExpense.ParentId == 0 ? matchedExpense.Id : matchedExpense.ParentId),
                PayerId = matchedExpense.PayerId,
                Author = User.Identity.Name,
                CreatedOn = matchedExpense.CreatedOn,
                ModifiedOn = DateTime.Now,
                Action = ActionType.Restored,
                IsActive = true,
                IsDeleted = false,
                ReceiptNumber = matchedExpense.ReceiptNumber,
                Amount = matchedExpense.Amount,
                PrivateAmount = matchedExpense.PrivateAmount,
                CustomerId = matchedExpense.CustomerId,
                ShopId = matchedExpense.ShopId,
                CreditCardId = matchedExpense.CreditCardId,
                IsCreditCardPayment = matchedExpense.IsCreditCardPayment,
                TextColor = matchedExpense.TextColor,
                WorkId = matchedExpense.WorkId,
            };

            this.context.Expenses.Add(restoredExpense);

            this.context.SaveChanges();

            return RedirectToAction("index");
        }

        //
        // Ajax GET: Expenses/History/parentId

        public PartialViewResult History(int id)
        {
            var history = this.context.Expenses
                .Where(e => e.ParentId == id || e.Id == id)
                .OrderBy(e => e.ModifiedOn)
                .Select(e => new ExpenseViewModel()
                {
                    Id = e.Id,
                    ParentId = e.ParentId,
                    PayerName = e.Payer.UserName,
                    Action = e.Action.ToString(),
                    CustomerName = e.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == e.WorkId).Name,
                    ShopName = e.Shop.Name,
                    CreatedOn = e.CreatedOn,
                    ReceiptNumber = e.ReceiptNumber,
                    Amount = e.Amount,
                    PrivateAmount = e.PrivateAmount,
                    IsCreditCardPayment = e.IsCreditCardPayment,
                    TextColor = e.TextColor,
                    Author = e.Author,
                    ModifiedOn = e.ModifiedOn,
                    CreditCardName = this.context.CreditCards.Where(c => c.Id == e.CreditCardId).FirstOrDefault().Name,
                }).ToList();

            return PartialView("", history);
        }
	}
}
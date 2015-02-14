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

    public class RevenuesController : BaseController
    {

        private string textColor;

        //
        // GET: /Revenues/
        public ActionResult Index()
        {
            var revenues = this.context.Revenues
                .Where(r => r.IsActive && !r.IsDeleted)
                .Select(r => new RevenueViewModel()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RecipientName = r.Recipient.UserName,
                    Action = r.Action.ToString(),
                    CustomerName = r.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == r.WorkId).Name,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount,
                    IsCreditCardPayment = r.IsCreditCardPayment,
                    CreditCardName = this.context.CreditCards.FirstOrDefault(c => c.Id == r.CreditCardId).Name,
                    TextColor = r.TextColor,
                    Author = r.Author,
                    HasInvoice = r.HasInvoice,
                    ModifiedOn = r.ModifiedOn,
                }).ToList();

            return View(revenues);
        }

        //
        // GET: /Revenues/ListDeleted
        public ActionResult ListDeleted()
        {
            var revenues = this.context.Revenues
                .Where(r => r.IsActive && r.IsDeleted)
                .Select(r => new RevenueViewModel()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RecipientName = r.Recipient.UserName,
                    Action = r.Action.ToString(),
                    CustomerName = r.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == r.WorkId).Name,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount,
                    IsCreditCardPayment = r.IsCreditCardPayment,
                    CreditCardName = this.context.CreditCards.FirstOrDefault(c => c.Id == r.CreditCardId).Name,
                    TextColor = r.TextColor,
                    Author = r.Author,
                    HasInvoice = r.HasInvoice,
                    ModifiedOn = r.ModifiedOn,
                }).ToList();

            return View(revenues);
        }

        private void InitializeDropDownFields(RevenueViewModel model)
        {
            model.Recipients = this.context.Users
                .Where(u => u.Roles.Any(r => r.Role.Name == "AccountHolder"))
                .Select(u => new SelectListItem()
                {
                    Text = u.UserName,
                    Value = u.Id,
                    Selected = (u.Id == model.RecipientId),
                }).ToList();

            model.Customers = this.context.Customers
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                    Selected = (c.Id == model.CustomerId),
                }).ToList();

            model.Works = this.context.Works
                .Where(w => w.IsActive)
                .Select(w => new SelectListItem()
                {
                    Text = w.Name,
                    Value = w.Id.ToString(),
                    Selected = (w.Id == model.WorkId),
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
        // GET: /Revenues/Create
        public ActionResult Create()
        {
            var model = new RevenueViewModel()
            {
                CreatedOn = DateTime.Now,
            };

            this.InitializeDropDownFields(model);

            return View(model);
        }

        // POST: Revenues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RevenueViewModel model)
        {
            model.Author = User.Identity.Name;

            if (model.Author.Equals("levend"))
            {
                this.textColor = ConfigurationManager.AppSettings["AccountHolderTextColor"];
            }

            if (ModelState.IsValid)
            {
                var revenue = new Revenue()
                {
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = DateTime.Now,
                    Recipient = this.context.Users.Find(model.RecipientId),
                    Author = model.Author,
                    Action = ActionType.Created,
                    IsActive = true,
                    Customer = this.context.Customers.Find(model.CustomerId),
                    WorkId = model.WorkId,
                    Amount = model.Amount,
                    IsCreditCardPayment = model.IsCreditCardPayment,
                    CreditCardId = model.IsCreditCardPayment ? model.CreditCardId : 0,
                    TextColor = this.textColor,
                };

                this.context.Revenues.Add(revenue);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            this.InitializeDropDownFields(model);

            return View(model);
        }

        //
        // GET: /Revenues/Update/id
        public ActionResult Update(int id)
        {
            var matchedRevenue = this.context.Revenues.Find(id);

            var model = new RevenueViewModel
            {
                Id = matchedRevenue.Id,
                //RecipientName = matchedRevenue.Recipient.Id,
                RecipientId = matchedRevenue.RecipientId,
                //CustomerName = matchedRevenue.Customer.Name,
                CustomerId = matchedRevenue.CustomerId,
                //WorkName = this.context.Works.Find(matchedRevenue.Id).Name,
                WorkId = matchedRevenue.WorkId,
                CreatedOn = matchedRevenue.CreatedOn,
                Amount = matchedRevenue.Amount,
                CreditCardId = matchedRevenue.CreditCardId,
                IsCreditCardPayment = matchedRevenue.IsCreditCardPayment,
            };

            this.InitializeDropDownFields(model);

            return View(model);
        }

        // POST: Revenues/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RevenueViewModel model)
        {
            model.Author = User.Identity.Name;

            if (model.Author.Equals("levend"))
            {
                this.textColor = ConfigurationManager.AppSettings["AccountHolderTextColor"];
            }

            if (ModelState.IsValid)
            {
                var oldRevenue = this.context.Revenues.Find(model.Id);
                oldRevenue.IsActive = false;

                var expence = new Revenue()
                {
                    ParentId = (oldRevenue.ParentId == 0 ? oldRevenue.Id : oldRevenue.ParentId),
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = DateTime.Now,
                    Recipient = this.context.Users.Find(model.RecipientId),
                    Author = model.Author,
                    Action = ActionType.Modified,
                    IsActive = true,
                    Customer = this.context.Customers.Find(model.CustomerId),
                    WorkId = model.WorkId,
                    Amount = model.Amount,
                    IsCreditCardPayment = model.IsCreditCardPayment,
                    CreditCardId = model.IsCreditCardPayment ? model.CreditCardId : 0,
                    TextColor = this.textColor,
                };

                this.context.Revenues.Add(expence);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            this.InitializeDropDownFields(model);

            return View(model);
        }

        //
        // AJAX: Revenues/GetCustomerWorks

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

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // AJAX: Revenues/HasInvoice

        public void HasInvoice(int id, bool invoice)
        {
            var matchedRevenue = this.context.Revenues.Find(id);

            matchedRevenue.HasInvoice = invoice;

            this.context.SaveChanges();
        }

        //
        // GET: Revenues/Delete/id

        public ActionResult Delete(int id)
        {
            var oleRevenue = this.context.Revenues.Find(id);
            oleRevenue.IsActive = false;

            var deletedRevenue = new Revenue()
            {
                ParentId = (oleRevenue.ParentId == 0 ? oleRevenue.Id : oleRevenue.ParentId),
                RecipientId = oleRevenue.RecipientId,
                Author = User.Identity.Name,
                CreatedOn = oleRevenue.CreatedOn,
                ModifiedOn = DateTime.Now,
                Action = ActionType.Deleted,
                IsActive = true,
                IsDeleted = true,
                Amount = oleRevenue.Amount,
                CustomerId = oleRevenue.CustomerId,
                CreditCardId = oleRevenue.CreditCardId,
                IsCreditCardPayment = oleRevenue.IsCreditCardPayment,
                TextColor = oleRevenue.TextColor,
                WorkId = oleRevenue.WorkId,
            };

            this.context.Revenues.Add(deletedRevenue);

            this.context.SaveChanges();

            return RedirectToAction("index");
        }

        //
        // GET: Revenues/Restore/id

        public ActionResult Restore(int id)
        {
            var oleRevenue = this.context.Revenues.Find(id);
            oleRevenue.IsActive = false;

            var restoredRevenue = new Revenue()
            {
                ParentId = (oleRevenue.ParentId == 0 ? oleRevenue.Id : oleRevenue.ParentId),
                RecipientId = oleRevenue.RecipientId,
                Author = User.Identity.Name,
                CreatedOn = oleRevenue.CreatedOn,
                ModifiedOn = DateTime.Now,
                Action = ActionType.Restored,
                IsActive = true,
                IsDeleted = false,
                Amount = oleRevenue.Amount,
                CustomerId = oleRevenue.CustomerId,
                CreditCardId = oleRevenue.CreditCardId,
                IsCreditCardPayment = oleRevenue.IsCreditCardPayment,
                TextColor = oleRevenue.TextColor,
                WorkId = oleRevenue.WorkId,
            };

            this.context.Revenues.Add(restoredRevenue);

            this.context.SaveChanges();

            return RedirectToAction("index");
        }

        //
        // Ajax GET: Revenues/History/parentId

        public PartialViewResult History(int id)
        {
            var history = this.context.Revenues
                .Where(r => r.ParentId == id || r.Id == id)
                .OrderBy(r => r.ModifiedOn)
                .Select(r => new RevenueViewModel()
                {
                    Id = r.Id,
                    ParentId = r.ParentId,
                    RecipientName = r.Recipient.UserName,
                    Action = r.Action.ToString(),
                    CustomerName = r.Customer.Name,
                    WorkName = this.context.Works.FirstOrDefault(wn => wn.Id == r.WorkId).Name,
                    CreatedOn = r.CreatedOn,
                    Amount = r.Amount,
                    IsCreditCardPayment = r.IsCreditCardPayment,
                    TextColor = r.TextColor,
                    Author = r.Author,
                    HasInvoice = r.HasInvoice,
                    ModifiedOn = r.ModifiedOn,
                    CreditCardName = this.context.CreditCards.Where(c => c.Id == r.CreditCardId).FirstOrDefault().Name,
                }).ToList();

            return PartialView("", history);
        }
	}
}
namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;
    using System.Data.Entity;

    public class CreditCardController : BaseController
    {
        //
        // GET: /CreditCard/Index/customerId
        public ActionResult Index(string id)
        {
            ViewBag.AccountHolder = this.context.Users.Find(id);
            var accountHolderId = User.Identity.GetUserId();

            ViewBag.CreditCards = this.context.CreditCards
                .Where(x => x.IsActive == true && x.AccountHolderId == accountHolderId)
                .Select(x => x).ToList();

            return View();
        }

        //
        // GET: /CreditCard/Create/customerId
        public ActionResult Create(string id)
        {
            var model = new CreditCardViewModel();

            return View(model);
        }

        //
        // POST: /CreditCard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreditCardViewModel model)
        {
            if(ModelState.IsValid)
            {
                var accountHolder = this.context.Users.Find(User.Identity.GetUserId());

                var creditCard = new CreditCard()
                {
                    Name = model.Name,
                    BankName = model.BankName,
                    CardNumber = model.CardNumber,
                    ValidThru = model.ValidThru,
                    IsActive = true,
                };

                accountHolder.CrediCards.Add(creditCard);

                this.context.Entry(accountHolder).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Index", new { id = User.Identity.GetUserId()});
            }            

            return View(model);
        }

        //
        // GET: /CreditCard/Update/id
        public ActionResult Update(int id)
        {
            var matchedCrediCard = this.context.CreditCards.Find(id);

            var model = new CreditCardViewModel()
            {
                CreditCardId = matchedCrediCard.Id,
                Name = matchedCrediCard.Name,
                BankName = matchedCrediCard.BankName,
                CardNumber = matchedCrediCard.CardNumber,
                ValidThru = matchedCrediCard.ValidThru,
            };

            return View(model);
        }

        //
        // POST: /CreditCard/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CreditCardViewModel model)
        {
            if(ModelState.IsValid)
            {
                var matchedCreditCard = this.context.CreditCards.Find(model.CreditCardId);

                matchedCreditCard.Name = model.Name;
                matchedCreditCard.BankName = model.BankName;
                matchedCreditCard.CardNumber = model.CardNumber;
                matchedCreditCard.ValidThru = model.ValidThru;

                this.context.Entry(matchedCreditCard).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
            }


            return View(model);
        }

        //
        // GET: /CreditCard/Restore/id
        public ActionResult Restore(int id)
        {
            var matchedCreditCard = this.context.CreditCards
                    .Find(id);

            matchedCreditCard.IsActive = true;

            this.context.Entry(matchedCreditCard).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
        }

        //
        // GET: /CreditCard/Delete/id
        public ActionResult Delete(int id)
        {
            var matchedCreditCard = this.context.CreditCards
                    .Find(id);

            matchedCreditCard.IsActive = false;

            this.context.Entry(matchedCreditCard).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index", new { id = User.Identity.GetUserId()});
        }

        //
        // GET: /Customers/InvalidCards/id
        public ActionResult InvalidCards()
        {
            ViewBag.AccountHolder = this.context.Users.Find(User.Identity.GetUserId());
            ViewBag.CreditCards = this.context.CreditCards
                .Where(x => x.IsActive == false)
                .Select(x => x).ToList();

            var creditCard = this.context.CreditCards
                .Where(c => c.IsActive == false)
                .Select(c => new CreditCardViewModel()
                {
                    CreditCardId = c.Id,
                    Name = c.Name,
                    BankName = c.BankName,
                    CardNumber = c.CardNumber,
                    ValidThru = c.ValidThru
                }).ToList();

            return View(creditCard);
        }
	}
}
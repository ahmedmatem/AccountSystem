namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;
    using AccountSystem.Common;

    public class WorksController : BaseController
    {
        //
        // GET: /Works/
        public ActionResult Index()
        {
            var works = this.context.Works
                .Where(w => w.IsActive == true)
                .OrderByDescending(w => w.StartTime)
                .Select(w => new WorkViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    StartTime = w.StartTime,
                    Price = w.Price,
                    OtherDetails = w.OtherDetails,
                    Customer = w.Customer
                }).ToList();

            return View(works);
        }

        //
        // GET: /Works/Create
        public ActionResult Create()
        {
            ViewBag.Customers = this.context.Customers
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = "" + c.Id
                }).ToList();

            return View();
        }

        //
        // POST: /Works/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newWork = new Work()
                {
                    StartTime = model.StartTime,
                    Name = model.Name,
                    Price = model.Price,
                    OtherDetails = model.OtherDetails,
                    CustomerId = model.Customer.Id,
                    IsActive = true
                };

                this.context.Works.Add(newWork);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Customers = this.context.Customers
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = "" + c.Id
                }).ToList();

            return View(model);
        }

        //
        // GET: /Works/Update/id
        public ActionResult Update(int id)
        {
            var matchedWork = this.context.Works
                .Where(w => w.Id == id)
                .Select(w => new WorkViewModel()
                {
                    Id = id,
                    Name = w.Name,
                    Customer = w.Customer,
                    StartTime = w.StartTime,
                    Price = w.Price,
                    OtherDetails = w.OtherDetails
                }).FirstOrDefault();

            ViewBag.Customers = this.context.Customers
               .Select(c => new SelectListItem()
               {
                   Text = c.Name,
                   Value = "" + c.Id,
               }).ToList();

            return View(matchedWork);
        }

        //
        // POST: /Works/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(WorkViewModel work)
        {
            if (ModelState.IsValid)
            {
                var matchedWork = this.context.Works
                    .Find(work.Id);

                matchedWork.CustomerId = work.Customer.Id;
                matchedWork.Name = work.Name;
                matchedWork.Price = work.Price;
                matchedWork.StartTime = work.StartTime;
                matchedWork.OtherDetails = work.OtherDetails;

                this.context.Entry(matchedWork).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(work);
        }

        //
        // GET: /Works/Restore/id
        public ActionResult Restore(int id)
        {
            var matchedWork = this.context.Works
                    .Find(id);

            matchedWork.IsActive = true;

            this.context.Entry(matchedWork).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Works/Delete/id
        public ActionResult Delete(int id)
        {
            var matchedWork = this.context.Works
                    .Find(id);

            matchedWork.IsActive = false;

            this.context.Entry(matchedWork).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Works/ShowDeleted
        public ActionResult ShowDeleted()
        {
            var works = this.context.Works
                .Where(w => w.IsActive == false)
                .OrderByDescending(w => w.StartTime)
                .Select(w => new WorkViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    StartTime = w.StartTime,
                    Price = w.Price,
                    OtherDetails = w.OtherDetails,
                    Customer = w.Customer
                }).ToList();

            return View(works);
        }

        //
        // AJAX GET: /Works/Work/id
        public JsonResult Work(int id)
        {
            var work = this.context.Works
                .Find(id);

            var customer = this.context.Customers
                .Find(work.CustomerId);

            var workInfo = new
            {
                WorkName = work.Name,
                CustomerName = customer.Name,
                Price = work.Price,
                StartTime = work.StartTime.Month + "/" + work.StartTime.Day + "/" + work.StartTime.Year,
                Details = work.OtherDetails
            };

            return Json(workInfo, JsonRequestBehavior.AllowGet);
        }
	}
}
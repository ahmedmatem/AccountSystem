namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;

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

        ////
        //// GET: /Customers/Update/id
        //public ActionResult Update(int id)
        //{
        //    var matchedCustomer = this.context.Customers
        //        .Where(c => c.Id == id)
        //        .Select(c => new CustomerViewModel()
        //        {
        //            Id = id,
        //            Name = c.Name,
        //            Email = c.Email,
        //            Address = c.Address,
        //            City = c.City,
        //            PostCode = c.PostCode,
        //            PhoneNumber = c.PhoneNumber,
        //            OtherDetails = c.OtherDetails
        //        }).FirstOrDefault();

        //    return View(matchedCustomer);
        //}

        ////
        //// POST: /Customers/Update
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Update(CustomerViewModel customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var matchedCustomer = this.context.Customers
        //            .Find(customer.Id);

        //        matchedCustomer.Name = customer.Name;
        //        matchedCustomer.Email = customer.Email;
        //        matchedCustomer.Address = customer.Address;
        //        matchedCustomer.City = customer.City;
        //        matchedCustomer.PostCode = customer.PostCode;
        //        matchedCustomer.PhoneNumber = customer.PhoneNumber;
        //        matchedCustomer.OtherDetails = customer.OtherDetails;

        //        this.context.Entry(matchedCustomer).State = EntityState.Modified;
        //        this.context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View(customer);
        //}

        ////
        //// GET: /Customers/Restore/id
        //public ActionResult Restore(int id)
        //{
        //    var matchedCustomer = this.context.Customers
        //            .Find(id);

        //    matchedCustomer.IsActive = true;

        //    this.context.Entry(matchedCustomer).State = EntityState.Modified;
        //    this.context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        ////
        //// GET: /Customers/Delete/id
        //public ActionResult Delete(int id)
        //{
        //    var matchedCustomer = this.context.Customers
        //            .Find(id);

        //    matchedCustomer.IsActive = false;

        //    this.context.Entry(matchedCustomer).State = EntityState.Modified;
        //    this.context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        ////
        //// GET: /Customers/ShowDeleted
        //public ActionResult ShowDeleted()
        //{
        //    var customers = this.context.Customers
        //        .Where(c => c.IsActive == false)
        //        .OrderByDescending(c => c.CreatedOn)
        //        .Select(c => new CustomerViewModel()
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Email = c.Email,
        //            Address = c.Address,
        //            City = c.City,
        //            PostCode = c.PostCode,
        //            PhoneNumber = c.PhoneNumber,
        //            OtherDetails = c.OtherDetails
        //        }).ToList();

        //    return View(customers);
        //}

        ////
        //// AJAX GET: /Customers/Customer/id
        //public JsonResult Customer(int id)
        //{
        //    var customer = this.context.Customers
        //        .Find(id);

        //    var customerInfo = new
        //    {
        //        Name = customer.Name,
        //        Email = customer.Email,
        //        Address = customer.Address,
        //        City = customer.City,
        //        PostCode = customer.PostCode,
        //        Phone = customer.PhoneNumber
        //    };

        //    return Json(customerInfo, JsonRequestBehavior.AllowGet);
        //}
	}
}
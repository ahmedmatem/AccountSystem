namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;
    using System.Data.Entity;

    public class CustomersController : BaseController
    {
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            var customers = this.context.Customers
                .Where(c => c.IsActive == true)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    City = c.City,
                    PostCode = c.PostCode,
                    PhoneNumber = c.PhoneNumber,
                    OtherDetails = c.OtherDetails
                }).ToList();

            return View(customers);
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCustomer = new Customer() 
                {
                    Name = model.Name,
                    CreatedOn = DateTime.Now,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    PostCode = model.PostCode,
                    PhoneNumber = model.PhoneNumber,
                    OtherDetails = model.OtherDetails,
                    IsActive = true
                };

                this.context.Customers.Add(newCustomer);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Customer/Update/id
        public ActionResult Update(int id)
        {
            var matchedCustomer = this.context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    City = c.City,
                    PostCode = c.PostCode,
                    PhoneNumber = c.PhoneNumber,
                    OtherDetails = c.OtherDetails
                }).FirstOrDefault();

            return View(matchedCustomer);
        }

        //
        // POST: /Customer/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                var matchedCustomer = this.context.Customers
                    .Find(customer.Id);

                matchedCustomer.Name = customer.Name;
                matchedCustomer.Email = customer.Email;
                matchedCustomer.Address = customer.Address;
                matchedCustomer.City = customer.City;
                matchedCustomer.PostCode = customer.PostCode;
                matchedCustomer.PhoneNumber = customer.PhoneNumber;
                matchedCustomer.OtherDetails = customer.OtherDetails;

                this.context.Entry(matchedCustomer).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Customer/Delete/id
        public ActionResult Delete(int id)
        {
            var matchedCustomer = this.context.Customers
                    .Find(id);

            matchedCustomer.IsActive = false;

            this.context.Entry(matchedCustomer).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}
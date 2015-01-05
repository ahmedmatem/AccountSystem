namespace AccountSystem.Web.Controllers
{
    using System;
    using System.Web.Mvc;    
    using System.Data.Entity;
    using System.Linq;

    using AccountSystem.Web.Models;
    using AccountSystem.Models;

    public class ShopsController : BaseController
    {
        //
        // GET: /Shops/
        public ActionResult Index()
        {
            var shops = this.context.Shops
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.Name)
                .Select(s => new ShopViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList();

            return View(shops);
        }

        //
        // GET: /Shops/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Shops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newShop = new Shop() 
                {
                    Name = model.Name,
                    IsActive = true
                };

                this.context.Shops.Add(newShop);
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Shops/Update/id
        public ActionResult Update(int id)
        {
            var matchedShop = this.context.Shops
                .Where(s => s.Id == id)
                .Select(s => new ShopViewModel()
                {
                    Id = id,
                    Name = s.Name,
                }).FirstOrDefault();

            return View(matchedShop);
        }

        //
        // POST: /Shops/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ShopViewModel shop)
        {
            if (ModelState.IsValid)
            {
                var matchedShop = this.context.Shops
                    .Find(shop.Id);

                matchedShop.Name = shop.Name;

                this.context.Entry(matchedShop).State = EntityState.Modified;
                this.context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(shop);
        }

        //
        // GET: /Shops/Restore/id
        public ActionResult Restore(int id)
        {
            var matchedShop = this.context.Shops
                    .Find(id);

            matchedShop.IsActive = true;

            this.context.Entry(matchedShop).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Shops/Delete/id
        public ActionResult Delete(int id)
        {
            var matchedShop = this.context.Shops
                    .Find(id);

            matchedShop.IsActive = false;

            this.context.Entry(matchedShop).State = EntityState.Modified;
            this.context.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Shops/ShowDeleted
        public ActionResult ShowDeleted()
        {
            var shops = this.context.Shops
                .Where(s => s.IsActive == false)
                .OrderBy(s => s.Name)
                .Select(s => new ShopViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList();

            return View(shops);
        }
	}
}
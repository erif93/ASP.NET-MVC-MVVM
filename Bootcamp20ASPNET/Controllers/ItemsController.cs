using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bootcamp20ASPNET.Models;
using Bootcamp20ASPNET.ViewModels;

namespace Bootcamp20ASPNET.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            return View(context.Items.Include("Suppliers").ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {

            IEnumerable<SelectListItem> mylist = context.Suppliers.Select(x => new SelectListItem

            {
                Value = x.Id.ToString(),
                Text = x.Name.ToString()

            });
            ViewBag.Preview = mylist;
           
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemVM _item)
        {
            if (ModelState.IsValid)
            {
                //Instansiasi untuk parsing dari VM ke Model
                Item item = new Item(_item);
               //Mengambil Value 1 row ke Supplier dari _item.Supplier 
                var getsupplier = context.Suppliers.Find(Convert.ToInt16(_item.Supplier));
                item.Suppliers = getsupplier;
                
                context.Items.Add(item);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_item);//return VM
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = context.Items.Include("Suppliers").SingleOrDefault(x=>x.Id==id);
            if (item == null)
            {
                return HttpNotFound();
            }
            var List = context.Suppliers.OrderBy(x => x.Name)
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = false
                }).ToArray();
          foreach(var getData in List)
            {
                if (getData.Value.Equals(item.Suppliers.Id.ToString()))
                {
                    getData.Selected = true;
                    break;
                }
            }
            ViewBag.Preview = List;
            var view = new ItemVM(item);

            //Item item = context.Items.Include("Suppliers").SingleOrDefault(x => x.Id == id);
            //ItemVM _item = new ItemVM();
            //var getsup = context.Suppliers.SingleOrDefault(x => x.Id == item.Suppliers.Id);
            //_item.Name = item.Name;
            //_item.Price = item.Price;
            //_item.Stock = item.Stock;
            //_item.Supplier = getsup.Id.ToString();
            // ViewBag.editid = id;

           
            return View(view);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemVM _item)
        {
            if (ModelState.IsValid)
            {
                //var getData= context.Items.Find(Convert.ToInt16(_item.Id));
                //getData.Name = _item.Name;
                //getData.Price = _item.Price;
                //getData.Stock = _item.Stock;
               
                    var push = context.Items.Include("Suppliers").SingleOrDefault(x => x.Id.Equals(_item.Id));
                    push.Update(_item);
                    int idSupplier = Convert.ToInt16(_item.Supplier);
                    var getSupplier = context.Suppliers.SingleOrDefault(x => x.Id.Equals(idSupplier));
                    push.Suppliers = getSupplier;
                    context.Entry(push).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            return View(_item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = context.Items.Find(id);
            context.Items.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

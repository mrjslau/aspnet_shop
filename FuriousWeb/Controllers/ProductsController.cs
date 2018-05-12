using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;
using System.Linq;

namespace FuriousWeb.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult GetProductsList(bool isPartial)
        {
            var products = db.Products.ToList();

            if (isPartial)
                return PartialView("Products", products);
            else
                return View("Products", products);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View(new CreateProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Code = viewModel.Code;
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;

                db.Products.Add(product);         
                db.SaveChanges();

                return RedirectToAction("../Home/Index");
            }

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();

            EditProductViewModel viewModel = new EditProductViewModel(product);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Find(viewModel.Id);
                product.Code = viewModel.Code;
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }
            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)         
                return HttpNotFound();

            var viewModel = new DeleteConfirmProductViewModel();
            viewModel.Code = product.Code;
            viewModel.Name = product.Name;
            viewModel.Description = product.Description;          

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("../Home/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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

        public ActionResult GetProductsListForUser(bool isPartial, string query)
        {
            var products = db.Products.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Code.ToLower().Contains(query.ToLower())).ToList();
            }

            if (isPartial)
                return PartialView("../Store/Products/Products", products);
            else
                return View("../Store/Products/Products", products);
        }

        public ActionResult GetProductsListForAdmin(bool isPartial, string query)
        {
            var products = db.Products.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(x => x.Name == query || x.Code == query).ToList();
            }

            if (isPartial)
                return PartialView("../Admin/Products/Products", products);
            else
                return View("../Admin/Products/Products", products);
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
            return View("../Admin/Products/Edit", product);
        }

        public ActionResult DetailsForUsers(int? id)
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
            return View("../Store/Products/Details", product);
        }

        public ActionResult Create()
        {
            return View("../Admin/Products/Create");
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

                return RedirectToAction("GetProductsListForAdmin", new { isPartial = false});
            }

            return View("../Admin/Products/Create", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();

            EditProductViewModel viewModel = new EditProductViewModel(product);

            return View("../Admin/Products/Edit", viewModel);
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
                return RedirectToAction("GetProductsListForAdmin", new { isPartial = false });
            }
            return View("../Admin/Products/Edit", viewModel);
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

            return View("../Admin/Products/Delete", viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("GetProductsListForAdmin", new { isPartial = false });
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

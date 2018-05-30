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

        public int GetProductCount(string query)
        {
            var products = db.Products.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Code.ToLower().Contains(query.ToLower())).ToList();
            }
            else
            {
                products = db.Products.ToList();
            }
            int count = products.Count();
            return count;
        }

        public ActionResult GetProductsListForUser(string query, int currentPage, bool isPartial)
        {
            int skip = (currentPage - 1)*12;
            int take = 12;
            var products = db.Products.ToList();
            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Code.ToLower().Contains(query.ToLower())).Skip(skip).Take(take).ToList();
            }
            else
            {
                products = db.Products.OrderBy(p => p.Id).Skip(skip).Take(take).ToList();
            }
            if (isPartial)
            {
                return PartialView("ProductsForUser", products);
            }
            else
            {
                return View("ProductsForUser", products);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetProductsListForAdmin(string query, int currentPage, bool  isPartial)
        {
            int skip = (currentPage - 1) * 12;
            int take = 12;
            var products = db.Products.ToList();
            if (!string.IsNullOrWhiteSpace(query))
                products = products.Where(x => x.Name.ToLower().Contains(query.ToLower()) || x.Code.ToLower().Contains(query.ToLower())).Skip(skip).Take(take).ToList();
            else
                products = db.Products.OrderBy(p => p.Id).Skip(skip).Take(take).ToList();
            if (isPartial)
                return PartialView("ProductsForAdmin", products);
            else
                return View("ProductsForAdmin", products);
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
            return View();
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
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    ModelState.AddModelError("Error", "Prekė su tokiu kodu jau egzistuoja.");
                    return View(viewModel);
                }

                return RedirectToAction("GetProductsListForAdmin", new { isPartial = false, query = "", currentPage = 1});
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
                try
                {
                    db.SaveChanges();
                }
                catch(System.Data.Entity.Infrastructure.DbUpdateException ex){
                    ModelState.AddModelError("Error", "Prekė su tokiu kodu jau egzistuoja.");
                    return View(viewModel);
                }
                return RedirectToAction("GetProductsListForAdmin", "Products", new { isPartial = false, query = "", currentPage = 1 });
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
            return RedirectToAction("GetProductsListForAdmin", "Products", new { isPartial = false, query = "", currentPage = 1 });
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

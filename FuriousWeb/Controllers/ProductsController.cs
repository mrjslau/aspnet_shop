using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System;
using System.Data.Entity.Infrastructure;

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
            var products = db.Products.Include(y => y.Images).DefaultIfEmpty().ToList();
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

            var viewModel = new DetailsViewModel();
            viewModel.Product = product;
            viewModel.ProductSecondaryImages = db.ProductImages.Where(x => x.ProductId == id && !x.IsMainImage).ToList();
            viewModel.ProductMainImage = db.ProductImages.SingleOrDefault(x => x.ProductId == id && x.IsMainImage);

            return View("../Store/Products/Details", viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductViewModel viewModel, HttpPostedFileBase mainImage, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Code = viewModel.Code;
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;
                product.Created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                db.Products.Add(product);

                var imagesToDownload = new List<HttpPostedFileBase>();

                if (mainImage != null)
                {
                    string relativePathToImage = Helper.GetRelativePathForResource(mainImage.FileName);
                    ProductImage productMainImage = Factories.ProductImageFactory.Create(product.Id, relativePathToImage, true);
                    db.ProductImages.Add(productMainImage);
                    imagesToDownload.Add(mainImage);
                }
           
                foreach (var image in images.Where(x => x != null))
                {
                    string relativePathToImage = Helper.GetRelativePathForResource(image.FileName);
                    ProductImage productImage = Factories.ProductImageFactory.Create(product.Id, relativePathToImage, false);
                    db.ProductImages.Add(productImage);
                    imagesToDownload.Add(image);
                }

                //sukuriam dar viena sarasa, kad klaidos atveju zinotume, kokias nuotraukas reikia istrint
                var downloadedImages = new List<HttpPostedFileBase>();
                try
                {
                    foreach (var img in imagesToDownload)
                    {
                        FileWorker.DownloadImage(img);
                        downloadedImages.Add(img);
                    }
                }
                catch (FileDownloadException)
                {
                    foreach (var img in downloadedImages)
                    {
                        string relativePathToImage = Helper.GetRelativePathForResource(img.FileName);
                        FileWorker.DeleteFile(relativePathToImage);
                    }

                    ModelState.AddModelError("Error", "Klaida išsaugant paveikslėlį.");
                    return View(viewModel);
                }

                try
                { 
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    ModelState.AddModelError("Error", "Prekė su tokiu kodu jau egzistuoja.");
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);
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

            List<ProductImage> secondaryImages = db.ProductImages.Where(x => x.ProductId == id && !x.IsMainImage).ToList();
            ProductImage mainImage = db.ProductImages.SingleOrDefault(x => x.ProductId == id && x.IsMainImage);

            EditProductViewModel viewModel = new EditProductViewModel(product, mainImage, secondaryImages);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductViewModel viewModel, byte[] rowVersion)
        {
            if (ModelState.IsValid)
            {
                Product product = db.Products.Find(viewModel.Id);
                product.Code = viewModel.Code;
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.Price = viewModel.Price;

                bool exceptionOccured = false;           
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.Entry(product).OriginalValues["RowVersion"] = rowVersion;
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("Error", "Įrašas buvo redaguotas kitu naudotoju. Išeikite iš redagavimo puslapio ir bandykite iš naujo");
                    exceptionOccured = true;
                }
                catch(DbUpdateException){
                    ModelState.AddModelError("Error", "Prekė su tokiu kodu jau egzistuoja.");
                    exceptionOccured = true;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    exceptionOccured = true;
                }

                if(exceptionOccured)
                {
                    viewModel.MainImage = db.ProductImages.SingleOrDefault(x => x.ProductId == viewModel.Id && x.IsMainImage);
                    viewModel.SecondaryImages = db.ProductImages.Where(x => x.ProductId == viewModel.Id && !x.IsMainImage).ToList();
                    return View(viewModel);
                }

                return RedirectToAction("GetProductsListForAdmin", "Products", new { isPartial = false, query = "", currentPage = 1 });
            }
            //!ModelState.IsValid
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

            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch(Exception) { }

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

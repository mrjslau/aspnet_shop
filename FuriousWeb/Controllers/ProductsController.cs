using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using FuriousWeb.Data;
using FuriousWeb.Models;
using FuriousWeb.BusinessLogic;
using System.Data.SqlClient;
using System.Configuration;
using FuriousWeb.Models.ViewModels;
using System.Linq;

namespace FuriousWeb.Controllers
{
    public class ProductsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult GetProductsListInStock(bool isPartial)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                List<DetailedProduct> products = Products.LoadDetailedProducts(conn);

                if(isPartial)
                    return PartialView("ProductsInStock", products);
                else
                    return View("ProductsInStock", products);
            }
            catch(Exception ex)
            {
                return Content($"<script>alert(\"Klaida: {ex.Message}\")</script>");
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult GetProductsList()
        {
            var products = db.Products.ToList();

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
            List<string> warehouseCodes = new List<string>();
            foreach(var warehouse in db.Warehouses)
            {
                warehouseCodes.Add(warehouse.Code);
            }

            var viewModel = new CreateDetailedProductViewModel(warehouseCodes);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDetailedProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Code = viewModel.Code;
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;

                db.Products.Add(product);

                if (viewModel.AddToStock)
                {
                    var productInStock = new ProductInStock();
                    productInStock.WarehouseId = db.Warehouses.Where(x => x.Code == viewModel.WarehouseCode).Select(x => x.Id).First();
                    productInStock.Price = viewModel.Price;
                    productInStock.Quantity = viewModel.Quantity;

                    db.Stock.Add(productInStock);
                }

                db.SaveChanges();

                return RedirectToAction("../Home/Index");
            }

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
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

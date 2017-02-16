using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;


namespace WebAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices productServices;

        public ProductController()
        {
            productServices = new ProductServices();
        }

        // GET: Product
        public IEnumerable<ProductEntity> Index()
        {
            var product = productServices.GetAllProducts();
            var productEntities = product as List<ProductEntity> ?? product.ToList();
            if (productEntities.Any())
            {
                return productEntities;
            }
            return null;
        }

        // GET: Product/Details/5
        public ProductEntity Details(int id)
        {
            var product = productServices.GetProductById(id);
            return product;
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

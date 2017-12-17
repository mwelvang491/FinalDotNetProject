﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using NorthwindClass.Models;

namespace Northwind.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product/Category
        public ActionResult Category()
        {
            // retrieve a list of all categories
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                return View(db.Categories.OrderBy(c => c.CategoryName).ToList());
            }
        }
        // GET: Product/Discount
        public ActionResult Discount()
        {
            // retrive a list of discounts 
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                // Filter by date
                DateTime now = DateTime.Now;
                return View(db.Discounts.Where(s => s.StartTime <= now && s.EndTime > now).ToList());
            }
        }
        // GET: Product/Search
        public ActionResult Search()
        {
            return View();
        }
        // POST: Product/SearchResults
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResults(FormCollection Form)
        {
            string SearchString = Form["SearchString"];
            ViewBag.Filter = "Product";
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                return View("Product", db.Products.Where(p => p.ProductName.Contains(SearchString) && p.Discontinued == false).OrderBy(p => p.ProductName).ToList());
            }
        }
        // GET: Product/Product/1
        public ActionResult Product(int? id)
        {
            // if there is no "category" id, return Http Bad Request
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                // save the selected category name to the ViewBag
                ViewBag.Filter = db.Categories.Find(id).CategoryName;
                // retrieve list of products
                return View(db.Products.Where(p => p.CategoryID == id && p.Discontinued == false).OrderBy(p => p.ProductName).ToList());
            }
        }

        public JsonResult FilterProducts(int? id, String SearchString, decimal PriceFilter)
        {
            using (NORTHWNDEntities db = new NORTHWNDEntities())
            {
                var Products = db.Products.Where(p => p.Discontinued == false).ToList();
                if (id != null)
                {
                    Products = Products.Where(p => p.CategoryID == id).ToList();
                }
                if (!String.IsNullOrEmpty(SearchString))
                {
                    Products = Products.Where(p => p.ProductName.Contains(SearchString)).ToList();
                }
                var ProductDTOs = (from p in Products.Where(p => p.UnitPrice >= 10)
                                   orderby p.ProductName
                                   select new
                                   {
                                       p.ProductID,
                                       p.ProductName,
                                       p.QuantityPerUnit,
                                       p.UnitPrice,
                                       p.UnitsInStock
                                   }).ToList();
                return Json(ProductDTOs, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChartData()
        {
            return View();
        }

    }
}


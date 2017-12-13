using NorthwindClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindClass.Controllers
{
    public class ProductController : Controller
    {
       List<Product> products;

        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        // GET: Product/Discount
        public ActionResult Discount()
        {
            return View();
        }

        public ActionResult CustomerInfo()
        {
            return View();
        }

        public ActionResult ProductSearch()
        {
            products = new List<Product>();
            products.Add(new Product { productName = "Fancy French Wine",           productPrice = 22.99, productId = 1 });
            products.Add(new Product { productName = "German Dark Chocolate Bars",  productPrice = 2.99, productId = 2 });
            products.Add(new Product { productName = "HoneyCrisp Apple",            productPrice =  .99, productId = 3 });
            products.Add(new Product { productName = "Carving Pumpkin",             productPrice = 3.99, productId = 4 });
            products.Add(new Product { productName = "Coffee Beans (1Lb)",          productPrice = 5.99, productId = 5 });
            products.Add(new Product { productName = "Organic Mac and Cheese",      productPrice = 4.99, productId = 6 });

            ViewBag.data = products;    

            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
	}
}
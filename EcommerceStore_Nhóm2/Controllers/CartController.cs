using EcommerceStore_Nhóm2.Models;
using EcommerceStore_Nhóm2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceStore_Nhóm2.Controllers
{
    public class CartController : Controller
    {
        private EcommerceStore_Group2Entities db = new EcommerceStore_Group2Entities();

        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                var cartService = GetCartService();
                cartService.GetCart().AddItem(product.ProductID, product.ImageList, product.ProductName, product.Price, quantity, product.Category.CategoryName);
            }

            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult updateQuantity(int id, int quantity)
        {
            var cartService = GetCartService();
            cartService.GetCart().UpdateQuantity(id, quantity); ;
            return RedirectToAction("Index");
        }
    }
}
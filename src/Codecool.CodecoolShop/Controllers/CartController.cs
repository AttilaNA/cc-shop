using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;


namespace Codecool.CodecoolShop.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        public ProductService ProductService { get; set; }

        public CartController()
        {
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(), SupplierDaoMemory.GetInstance());
        }


        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = getTotalPrice();
            return View();
        }

        [Route("buy/{id}")]
        public void Buy(string id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = ProductService.GetProduct(int.Parse(id)), Quantity=1});
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = ProductService.GetProduct(int.Parse(id)), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
          
        }

        [Route("decrease/{id}")]
        public void Decrease(string id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity--;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
        }
        [Route("set/{id}/{count}")]
        public void Set(string id,string count)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {

                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity= int.Parse(count);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }
        }


        [Route("remove/{id}")]
        public string Remove(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return "Removed";
        }

        private int isExist(string id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(int.Parse(id)))
                {
                    return i;
                }
            }
            return -1;
        }

        [Route("item-count")]
        public int GetItemCountInCart()
        {
            if (HttpContext == null)
            {
                return 0;
            }

            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return 0;
            }
            int count = 0;
            for (int i = 0; i < cart.Count; i++)
            {
                count += cart[i].Quantity;
            }
            return count;
        }

        [Route("total-price")]
        public decimal getTotalPrice()
        {
            decimal sum = 0; 
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") != null)
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                foreach (var item in cart)
                {
                    sum+= item.Product.DefaultPrice * item.Quantity;
                }
                
            }

            return sum;

        }

    }
}

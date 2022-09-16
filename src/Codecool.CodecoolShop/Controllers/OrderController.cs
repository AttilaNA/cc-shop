using System.Collections.Generic;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;


namespace Codecool.CodecoolShop.Controllers;

public class OrderController : Controller
{
    [HttpPost]
    public IActionResult Index(Order obj)
    {
        SessionHelper.SetObjectAsJson(HttpContext.Session, "order", obj);

        return RedirectToAction("PaymentForm");
    }
    
    public IActionResult CheckoutForm() => View();

    public IActionResult PaymentForm()
    {
        ViewBag.total = GetTotalPrice();
        return View();
    }

    public IActionResult Confirmation()
    {
        Order paidOrder = CollectOrderInformation();
        ViewBag.cart = paidOrder.OrderedItems;
        ViewBag.contact = paidOrder;
        ViewBag.total = GetTotalPrice();
        return PartialView();
    }

    public Order CollectOrderInformation()
    {
        List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
        Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
        order.OrderedItems = cart;
        JsonHelper.AppendJsonFile(order,"paidOrder");

        return order;
    }
    
    public decimal GetTotalPrice()
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
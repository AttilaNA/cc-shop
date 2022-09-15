﻿using System.Collections.Generic;
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

    public void RecordOrder()
    {
        
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
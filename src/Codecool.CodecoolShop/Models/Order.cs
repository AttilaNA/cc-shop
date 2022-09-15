using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models;

public class Order
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingZip { get; set; }
    public List<CartItem> OrderedItems { get; set; }
}
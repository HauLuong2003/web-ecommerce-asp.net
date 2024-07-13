using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public  class CartItem
{
    public int PId { get; set; }
    public string cartId { get; set; }
    public int Quantity { get; set; }
    public float Price {  get; set; }
    public float totalPrice => Quantity*Price;
}

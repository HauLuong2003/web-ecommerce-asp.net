using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int? CartId { get; set; }

    public int? PId { get; set; }

    public int? Quantity { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Product? PIdNavigation { get; set; }
}

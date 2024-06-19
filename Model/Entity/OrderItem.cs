using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int PId { get; set; }

    public int OderId { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public virtual Oder Oder { get; set; } = null!;

    public virtual Product PIdNavigation { get; set; } = null!;
}

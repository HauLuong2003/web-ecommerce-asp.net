using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class OrderStatus
{
    public int OrderSatatusId { get; set; }

    public string? Status { get; set; }

    public virtual Oder OrderSatatus { get; set; } = null!;
}

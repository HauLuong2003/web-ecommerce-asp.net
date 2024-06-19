using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class OrderCancellationReason
{
    public int ReasonId { get; set; }

    public string Reason { get; set; } = null!;

    public virtual ICollection<Oder> Oders { get; set; } = new List<Oder>();
}

using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Payment
{
    public int PaymentId { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Method { get; set; } = null!;

    public double TotalMoney { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Oder> Oders { get; set; } = new List<Oder>();
}

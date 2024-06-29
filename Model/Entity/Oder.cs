using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Oder
{
    public int OrderId { get; set; }

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int Status { get; set; }

    public DateTime OderDate { get; set; }

    public double TotalMoney { get; set; }

    public double ShippingCost { get; set; }

    public int? UserId { get; set; }

    public int PaymentId { get; set; }

    public int? OrderCancellationReasonId { get; set; }

    public virtual OrderCancellationReason? OrderCancellationReason { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual User? User { get; set; }
}

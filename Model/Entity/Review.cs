using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int PId { get; set; }

    public string Comment { get; set; } = null!;

    public DateOnly CreatAt { get; set; }

    public virtual Product PIdNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

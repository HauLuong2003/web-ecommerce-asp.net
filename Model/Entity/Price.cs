﻿using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Price
{
    public int PriceId { get; set; }

    public double Price1 { get; set; }

    public int PId { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual Product PIdNavigation { get; set; } = null!;
}

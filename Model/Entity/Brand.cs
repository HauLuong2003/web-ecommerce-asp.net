using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? BrandLogo { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

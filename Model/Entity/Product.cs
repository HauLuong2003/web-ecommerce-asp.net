using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Product
{
    public int PId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public DateOnly? CreatAt { get; set; }

    public DateOnly? UpdateAt { get; set; }

    public bool Featured { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual ICollection<Galery> Galeries { get; set; } = new List<Galery>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}

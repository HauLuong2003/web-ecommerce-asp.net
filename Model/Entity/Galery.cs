using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Galery
{
    public int ImageId { get; set; }

    public string Image { get; set; } = null!;

    public int PId { get; set; }

    public virtual Product PIdNavigation { get; set; } = null!;
}

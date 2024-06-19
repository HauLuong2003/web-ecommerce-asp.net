using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class Detail
{
    public int DetailId { get; set; }

    public string? SeriesLaptop { get; set; }

    public string? PartNumber { get; set; }

    public string? Color { get; set; }

    public string? CpuGeneration { get; set; }

    public string? Screen { get; set; }

    public string? Storage { get; set; }

    public string? ConnectorPort { get; set; }

    public string? WirelessConnection { get; set; }

    public string? Keyboard { get; set; }

    public string? Os { get; set; }

    public string? Size { get; set; }

    public string? Pin { get; set; }

    public string? Weight { get; set; }

    public int PId { get; set; }

    public virtual Product PIdNavigation { get; set; } = null!;
}

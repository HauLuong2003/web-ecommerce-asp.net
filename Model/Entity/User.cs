using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Sex { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateOnly Birthdate { get; set; }

    public string Avata { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Oder> Oders { get; set; } = new List<Oder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;
}

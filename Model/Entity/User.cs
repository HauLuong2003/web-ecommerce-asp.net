using System;
using System.Collections.Generic;

namespace Web_Ecommerce_Server.Model.Entity;

public partial class User
{
    public int UserId { get; set; }

    public string? Fullname { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    public string? VerificationToken { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public string? Avata { get; set; }

    public int RoleId { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime UpdateAt { get; set; }

    public virtual ICollection<Oder> Oders { get; set; } = new List<Oder>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;
}

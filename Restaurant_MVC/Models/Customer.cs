using System;
using System.Collections.Generic;

namespace Restaurant_MVC.Models;

public partial class Customer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<ProductCustomer> ProductCustomers { get; set; } = new List<ProductCustomer>();

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}

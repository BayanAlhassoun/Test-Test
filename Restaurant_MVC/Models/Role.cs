﻿using System;
using System.Collections.Generic;

namespace Restaurant_MVC.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}

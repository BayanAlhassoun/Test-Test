using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_MVC.Models;

public partial class Category
{

    public decimal Id { get; set; }

    [Required]
    public string? CategoryName { get; set; }
    [DisplayName("Image Path")]
    public string? ImagePath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}


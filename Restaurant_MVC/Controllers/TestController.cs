using Microsoft.AspNetCore.Mvc;
using Restaurant_MVC.Models;

namespace Restaurant_MVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>()
            {
                new Category { Id = 1, CategoryName = "Main Meal"},
                new Category { Id = 2, CategoryName = "Salad"},
                new Category { Id = 3, CategoryName = "Dessert"},
            };
            return View(categories);
        }        
        
        public IActionResult Home()
        {
            return View();
        }
    }
}

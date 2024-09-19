using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_MVC.Models;

namespace Restaurant_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            ViewBag.categories = _context.Categories.ToList();
            ViewBag.Customers = _context.Customers.ToList();

            ViewData["Products"] = _context.Products.ToList();

            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.TotalPrices = _context.Products.Sum(x => x.Price);


            var id = HttpContext.Session.GetInt32("AdminId");

            var user = _context.Customers.Where(x => x.Id == id).SingleOrDefault();
            return View(user);
        }

        public IActionResult Index2()
        {
            var Categories = _context.Categories.ToList();
            var Customers = _context.Customers.ToList();
            var Products = _context.Products.ToList();

            var finalResult = Tuple.Create<IEnumerable<Category>, IEnumerable<Customer>, IEnumerable<Product>>(Categories, Customers, Products);



            ViewBag.ProductsCount = _context.Products.Count();
            ViewBag.TotalPrices = _context.Products.Sum(x => x.Price);


            //var id = HttpContext.Session.GetInt32("AdminId");

            //var user = _context.Customers.Where(x => x.Id == id).SingleOrDefault();
            return View(finalResult);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(" ", " ");
        }

        public IActionResult JoinTables()
        {
            var Categories = _context.Categories.ToList();
            var Products = _context.Products.ToList();
            var Customers = _context.Customers.ToList();
            var ProductCustomers = _context.ProductCustomers.ToList();

            var result = from c in Customers
                         join pc in ProductCustomers on c.Id equals pc.CustomerId
                         join p in Products on pc.ProductId equals p.Id
                         join cat in Categories on p.CategoryId equals cat.Id
                         select new JoinTables { Product = p, Category = cat, Customer = c, ProductCustomer = pc };
            return View(result);
        }

        public IActionResult Search()
        {

            var result = _context.ProductCustomers.Include(x => x.Product).Include(x => x.Customer).ToList();
            ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

            return View(result);
        }

        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var result = _context.ProductCustomers.Include(x => x.Product).Include(x => x.Customer).ToList();

            if (startDate == null && endDate == null)
            {
                ViewBag.TotalPrice = result.Sum(x=>x.Product.Price * x.Quantity);
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {

                result = result.Where(x => x.DateFrom.Value.Date >= startDate).ToList();
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }
            else if (startDate == null && endDate !=null)
            {

                result = result.Where(x=>x.DateFrom.Value.Date <= endDate).ToList();
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }
            else
            {

                result = result.Where(x => x.DateFrom.Value.Date >= startDate && x.DateFrom.Value.Date <= endDate).ToList();
                ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
        }
    }
}

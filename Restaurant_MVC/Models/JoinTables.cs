namespace Restaurant_MVC.Models
{
    public class JoinTables
    {
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
        public ProductCustomer ProductCustomer { get; set; }
    }
}

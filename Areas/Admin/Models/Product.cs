using System.ComponentModel.DataAnnotations;

namespace ASM2.Areas.Admin.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public double UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}

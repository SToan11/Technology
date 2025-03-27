namespace ASM2.Models
{
    public class Models_Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public double UnitPrice { get; set; }
        public int AvailableQuantity
        { get; set;}
        public string Image { get; set; }
    }
}

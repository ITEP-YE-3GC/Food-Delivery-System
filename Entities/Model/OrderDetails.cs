

namespace OrderService.Entities.Model
{
    public class OrderDetails
    {
        public Guid OrderID { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string? CustomizationNote { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

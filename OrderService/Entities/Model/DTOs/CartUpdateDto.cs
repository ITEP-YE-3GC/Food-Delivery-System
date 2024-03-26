
namespace OrderService.Entities.Model.DTOs
{
    public class CartUpdateDto
    {
        public long CustomerID { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; } = 0;
        public int Quantity { get; set; }

        public bool Status { get; set; } = true;

        public List<CartCustomization>? CartCustomization { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }

}

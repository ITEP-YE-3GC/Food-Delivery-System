using OrderService.Utilities;

namespace OrderService.Entities.Model.DTOs
{
    public class CartDTO
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
   
}

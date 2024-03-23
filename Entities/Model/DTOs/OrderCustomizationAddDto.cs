using OrderService.Utilities;

namespace OrderService.Entities.Model.DTOs
{
    public class OrderCustomizationAddDto
    {
        public int CustomizationID { get; set; }

        public double Price { get; set; } = 0;

        public int Quantity { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

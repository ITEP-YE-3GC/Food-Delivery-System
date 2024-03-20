using OrderService.Utilities;

namespace OrderService.Entities.Model
{
    public class CartCustomization
    {
        public long SeqID { get; set; }
        public int CustomizationID { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;

    }
}

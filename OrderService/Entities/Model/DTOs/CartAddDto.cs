
namespace OrderService.Entities.Model.DTOs
{
    public class CartAddDto
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; } = 0;
        [Range(1, 1000, ErrorMessage = "Quantity must be 1 or greater.")]
        [DefaultValue(1)]
        public int Quantity { get; set; }
        public bool Status { get; set; } = true;

        public List<CartCustomizationAddDto>? CartCustomization { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

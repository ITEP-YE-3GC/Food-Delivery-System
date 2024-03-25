
namespace OrderService.Entities.Model
{

    public class Cart: BaseModelID<Guid>
    {
        public int RestaurantId { get; set; }
        public long CustomerID { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; } = 0;
        public int Quantity { get; set; }
        public bool Status { get; set; } = true;

        public string? CustomizationNote { get; set; }

        public List<CartCustomization>? CartCustomization { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;

        public Cart()
        {
            Id = Guid.NewGuid(); // Initialize CartID with a new Guid
        }
    }
}

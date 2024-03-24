
namespace OrderService.Entities.Model
{
    public class Order : BaseModelID<Guid>
    {
        public long CustomerID { get; set; }
        public int RestaurantID { get; set; }
        public int DriverID { get; set; }
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime OrderDate { get; set; }

        public int PaymentID { get; set; }
        public Payment Payment { get; set; }
        public int OrderStatusID { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            Id = Guid.NewGuid(); // Initialize CartID with a new Guid
        }
    }
}

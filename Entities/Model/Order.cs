using OrderService.Entities.Model.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities.Model
{
    public class Order : BaseModelID<Guid>
    {
        public long CustomerID { get; set; }
        public int DriverID { get; set; }
        public int AddressID { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedDate { get; set; }
        public DateTime OrderDate { get; set; }

        public int PaymentID { get; set; }
        //public Payments Payments { get; set; }

        public int OrderStatusID { get; set; }
        //public OrderStatus OrderStatus { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

        public List<OrderCustomization>? OrderCustomization { get; set; }

        public Order()
        {
            Id = Guid.NewGuid(); // Initialize CartID with a new Guid
        }
    }
}

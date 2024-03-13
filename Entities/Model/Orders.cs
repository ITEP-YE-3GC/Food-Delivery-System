using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities.Model
{
    public class Orders
    {
        [Key]
        [Column("OrderID")]
        [DatabaseGenerated(databaseGeneratedOption:DatabaseGeneratedOption.None)]
        public long OrderID { get; set; }
        public int CustomerID { get; set; }
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
    }
}

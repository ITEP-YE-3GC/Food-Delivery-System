

using OrderService.Utilities;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model
{
    public class OrderDetails
    {
        [Key]
        public long Seq { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public long OrderID { get; set; }
        //public Orders? Order { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

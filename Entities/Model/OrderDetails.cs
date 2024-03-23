

using OrderService.Utilities;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model
{
    public class OrderDetails
    {
        public long Seq { get; set; }

        public Guid OrderID { get; set; }
        public int ProductID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

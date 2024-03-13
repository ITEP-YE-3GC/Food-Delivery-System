using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrderService.Utilities;

namespace OrderService.Entities.Model
{
    public class Carts
    {
        [Key]
        [Column("Seq")]
        public long Seq { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public bool ValidQuantity => Quantity >=Constants.ValidQuantity;
    }
}

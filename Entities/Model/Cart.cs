using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrderService.Utilities;

namespace OrderService.Entities.Model
{
    public class Cart
    {
        // ============================
        // Change by: Anwar Hamzah
        // Commit the [Seq] attribute and
        // create a composite key of [CustomerID, ProductID]
        // Change class name from Carts => Cart
        // ============================

        //[Key]
        //[Column("Seq")]
        //public long Seq { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public bool ValidQuantity => Quantity >= Constants.ValidQuantity;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OrderService.Utilities;
using System.ComponentModel;

namespace OrderService.Entities.Model.DTOs.RequestDTO
{
    public class CartAddDTO
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        [Range(1, 1000, ErrorMessage = "Quantity must be 1 or greater.")]
        [DefaultValue(1)]
        public int Quantity { get; set; }


      

    }
}

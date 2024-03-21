using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model.DTOs
{
    public class OrderAddDTO
    { 
        public long OrderID { get; set; }
        public int CustomerID { get; set; }
        public int DriverID { get; set; }
        public int AddressID { get; set; }
        public int PaymentID { get; set; }
       //
       //
       //
       
    }
}

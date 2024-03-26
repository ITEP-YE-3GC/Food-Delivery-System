
namespace OrderService.Entities.Model.DTOs
{
    public class OrderAddDto
    { 
        public long CustomerID { get; set; }
        public int RestaurantID { get; set; }
        public int DriverID { get; set; }
        public int AddressID { get; set; }
        public int PaymentID { get; set; }
        public int StatusID { get; set; }

        public List<OrderDetailsAddDto>? OrderDetails { get; set; }
        //
        //
        //

    }
}

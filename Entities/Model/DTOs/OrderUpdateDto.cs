using OrderService.Utilities;

namespace OrderService.Entities.Model.DTOs
{
    public class OrderUpdateDto
    {
        public int CustomerID { get; set; }
        public int DriverID { get; set; }
        public int AddressID { get; set; }
        public int PaymentID { get; set; }
        public int StatusID { get; set; }

        public List<OrderDetailsAddDto>? OrderDetails { get; set; }
        public List<OrderCustomizationAddDto>? OrderCustomization { get; set; }
    }
}

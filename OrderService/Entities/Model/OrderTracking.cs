
namespace OrderService.Entities.Model
{
    public class OrderTracking
    {
        public long Seq { get; set; }
        public long OrderID { get; set; }
        public long OrderStatusID { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public bool IsDone { get; set; }
       
    }
}

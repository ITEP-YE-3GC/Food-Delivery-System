using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model
{
    public class OrderTracking
    {
        [Key]
        public long Seq { get; set; }
        public long OrderID { get; set; }
        public long OrderStatusID { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public bool IsDone { get; set; }
       
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        [Column("StatusID")]
        public int StatusID { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        [StringLength(10, ErrorMessage = "Name can't be longer than 10 characters")]
        public string Name { get; set; }
    }
}

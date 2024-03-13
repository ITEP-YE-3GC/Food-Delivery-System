
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Entities.Model
{
    
    [Table("Payments")]
    public class Payments
    {
        [Key]
        [Column("PaymentID")]
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        [StringLength(10, ErrorMessage = "Name can't be longer than 10 characters")]
        public string Name { get; set; }
    }
}

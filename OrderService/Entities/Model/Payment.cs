

namespace OrderService.Entities.Model
{
    public class Payment
    {
        [Key]
        [Column("PaymentID")]
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        [StringLength(10, ErrorMessage = "Name can't be longer than 10 characters")]
        public required string Name { get; set; }
    }
}

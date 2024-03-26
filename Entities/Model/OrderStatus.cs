
namespace OrderService.Entities.Model
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        [Column("StatusID")]
        public int StatusID { get; set; }
        [Required(ErrorMessage = "Name  is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 10 characters")]
        public required string Name { get; set; }

        public int NextStep { get; set; } = 0;

        public bool Automated { get; set; } = false;
    }
}

using System.ComponentModel.DataAnnotations;
using static OrderService.Utilities.Constants;

namespace OrderService.Entities.Model
{
    public class User
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "User Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "User Password is required")]
        public string? Password { get; set; }
        [EnumDataType(typeof(UserRole), ErrorMessage = "User Role Must be: user or admin")]
        [Required]
        public UserRole Role { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities.Model.BaseEntity
{
    public class BaseModelID<T>
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

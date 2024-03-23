using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Entities.Model.BaseEntity
{
    public class BaseModelID<T>
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }

        private DateTime _createDate = DateTime.UtcNow;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value.ToUniversalTime(); }
        }
    }
}

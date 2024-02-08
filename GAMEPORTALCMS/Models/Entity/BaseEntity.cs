using System.ComponentModel.DataAnnotations;

namespace GAMEPORTALCMS.Models.Entity
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}

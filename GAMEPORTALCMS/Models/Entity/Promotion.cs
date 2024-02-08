using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("Promotion", Schema = "dbo")]
    public class Promotion:BaseEntity<int>
    {
        public string? PromotionName { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? EventUrl { get; set; }
        public bool IsActive { get; set; }
        public int Client { get; set; }
        public int Serial { get; set; }

        public string? ClientValueDetails { get; set;}

    }
}

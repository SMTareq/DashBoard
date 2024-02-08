using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("GameCategory", Schema = "dbo")]
    public class GameCategory : BaseEntity<int>
    {
        public string?  CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
        public int Serial { get; set; }

        public string? IconURL { get; set; }
    }
}

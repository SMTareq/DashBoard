using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("GameType", Schema = "dbo")]
    public class GameType : BaseEntity<int>
    {
        public string? TypeName { get; set; }
        public bool IsActive { get; set; }
    }
}

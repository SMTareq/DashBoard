using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("PatchGames", Schema = "dbo")]
    public class PatchGame : BaseEntity<int>
    {
        public int GamePatchId { get; set; }
        public int GameId { get; set; }
        public int Serial { get; set; }
    }
}

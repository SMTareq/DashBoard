using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("GamePatch", Schema = "dbo")]
    public class GamePatch : BaseEntity<int>
    {
        public string? PatchName { get; set; }
        public bool IsActive { get; set; }
        public string? ImageURL { get; set; }

        public int Type { get; set; }
        public int Client { get; set; }
        public int Serial { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{

    [Table("Billboard", Schema = "dbo")]
    public class Billboard : BaseEntity<int>
    {

        public int GameId { get; set; }
        public string? ImageURL { get; set; }
        public int GameType { get; set; }
        public int Client { get; set; }
        public bool IsActive { get; set; }

        public int Serial { get; set; }

        public string? ClientValueDetails { get; set; }
    }
}

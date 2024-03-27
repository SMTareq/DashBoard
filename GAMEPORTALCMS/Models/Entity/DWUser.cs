using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("DWUser", Schema = "dbo")]
    public class DWUser
    {
        public int? uid { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
    }
}

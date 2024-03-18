using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{
    [Table("EBL_Migration", Schema = "dbo")]
    public class EBL_Migration
    {
        public int DWDOCID { get; set; }
        public string? DWNAME { get; set; }
        public string? STATUS { get; set; }
        public string? DOCUMENT_NAME { get; set; }
    }
}

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
        public DateTime DWSTOREDATETIME { get; set; }
        public string? ACCOUNT_NO { get; set; }
        public string? M_PRODUCT_TYPE { get; set; }
        public string? M_CREATED_DATE { get; set; }
        public string? DOCUMENT_TYPE { get; set; }
        public string? DATA_CLASS { get; set; }
        public string? M_CIF { get; set; }
        public string? M_ACCOUNT_NO { get; set; }
        public string? M_DOCUMENT_NAME { get; set; }

        public string? PRODUCT_TYPE { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{

    [Table("EBL_POC", Schema = "dbo")]
    //[Table("_EBL_POC", Schema = "dbo")]
    public class _EBL_POC
    {   
            public int DWDOCID { get; set; }
            public string? DWNAME { get; set; }
            public string? STATUS { get; set; }
            public string? DOCUMENT_NAME { get; set; }
            public DateTime DWSTOREDATETIME { get; set; }
            public string? ACCOUNT_NO { get; set; }
            public string? PRODUCT_TYPE { get; set; }
            public string? M_CREATED_DATE { get; set; }
            public string? DATA_CLASS { get; set; }
            public string? CIF { get; set; }
        public string? USER { get; set; }
        public string? WF_CREATOR { get; set; }
        public string? WF_CREATOR_COMMENTS { get; set; }
        public string? WF_MAKER { get; set; }
        public string? WF_MAKER_COMMENTS { get; set; }
        public string? WF_CHECKER { get; set; }
        public string? WF_CHECKER_COMMENTS { get; set; }
        public string? WF_MANAGER { get; set; }
        public string? WF_MANAGER_COMMENTS { get; set; }
        public string? BRANCH_CODE { get; set; }

    }
}

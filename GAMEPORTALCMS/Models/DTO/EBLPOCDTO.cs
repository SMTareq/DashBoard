namespace GAMEPORTALCMS.Models.DTO
{
    public class EBLPOCDTO
    {
        public int DWDOCID { get; set; }
        public string? DATA_CLASS { get; set; }
        public string? ACCOUNT_NO { get; set; }    
        public string? STATUS { get; set; }
        public string? DOCUMENT_NAME { get; set; }
        public string? PRODUCT_TYPE { get; set; }

        public string? BranchCode { get; set; }
        public string? CIF { get; set; }

        public DateTime DWSTOREDATETIME { get; set; }   
    }
}

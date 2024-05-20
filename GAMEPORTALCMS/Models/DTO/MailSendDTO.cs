namespace GAMEPORTALCMS.Models.DTO
{
    public class MailSendDTO
    {
        public string MyProperty { get; set; }
        public List<MailBody> mailBodies { get; set; }
    }

    public class MailBody
    {
        public int DWDOCID { get; set; }
        public string? M_DATA_CLASS { get; set; }
        public string? M_ACCOUNT_NO { get; set; }
        public string? DocumentName { get; set; }
        public string? BranchCode { get; set; }
        public string? M_STATUS { get; set; }     
        public string? STATUS { get; set; }
        public string? ProductType { get; set; }
        public string? Cif { get; set; }
        public DateTime? DWSTOREDATETIME { get; set; }
                                                                                                                                                                                      
    }

}

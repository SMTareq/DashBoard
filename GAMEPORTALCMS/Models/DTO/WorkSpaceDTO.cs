namespace GAMEPORTALCMS.Models.DTO
{
    public class WorkSpaceDTO
    {
        public List<WorkSpaceUserDTO> spaceUserDTOs { get; set; }
        public List<WorkSpaceStatusDTO> workSpaceStatusDTOs { get; set; }
        public List<WorkSpaceAccountTypeDTO> workSpaceAccountTypeDTOs { get; set; }
    }

    public class WorkSpaceUserDTO
    {
        public string? User { get; set; }
    }

    public class WorkSpaceStatusDTO
    {
        public string? Status { get; set;}
    }

    public class WorkSpaceAccountTypeDTO
    {
        public string? AccountType { get; set; }
    }


    public class WorkSpaceTotalRecord
    {
        public int DWDOCID { get; set; }
        public string? DATA_CLASS { get; set; }
        public string? ACCOUNT_NO { get; set; }
        public string? STATUS { get; set; }
        public string? DocumentName { get; set; }
        public string? ProductType { get; set; }

        public string? BranchCode { get; set; }
        public string? CIF { get; set; }

      
    }


}

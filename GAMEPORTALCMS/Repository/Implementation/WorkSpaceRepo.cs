using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Response;
namespace GAMEPORTALCMS.Repository.Implementation
{
    public class WorkSpaceRepo
    {
        private readonly AppDBContext _dbContext;
        private readonly LoginDBContext _loginDBContext;

        public WorkSpaceRepo(AppDBContext appDBContext, LoginDBContext loginDBContext)
        {
             _dbContext = appDBContext;
            _loginDBContext = loginDBContext;
        }

        public async Task<int> TotalRecordOfPerCabinateAsync(string? Cabinate_Id)
        {
            int totalCount = 0;

            try
            {
                if (Cabinate_Id == null)
                {
                    throw new ArgumentNullException(nameof(Cabinate_Id), "Cabinate_Id cannot be null");
                }

                switch (Cabinate_Id)
                {
                    case "1":
                        totalCount = await _dbContext.EBL_Migrations.CountAsync();
                        break;

                    case "2":
                        totalCount = await _dbContext._EBL_POCs.CountAsync();
                        break;

                    default:
                        throw new ArgumentException("Invalid Cabinate_Id", nameof(Cabinate_Id));
                }
            }
            catch (Exception e)
            {
                // Log the exception (if logging is set up)
                // Example: _logger.LogError(e, "An error occurred while counting records");
                throw; // Rethrow the exception to preserve the stack trace
            }

            return totalCount;
        }

        public DashBoardBanarDTO TotalRecordOfPerCabinate(string? Cabinate_Id)
        {

            DashBoardBanarDTO dTO = new DashBoardBanarDTO();

            try
            {
                if (Cabinate_Id == null)
                {
                    throw new ArgumentNullException(nameof(Cabinate_Id), "Cabinate_Id cannot be null");
                }

                switch (Cabinate_Id)
                {
                    case "1":
                       // totalCount =  _dbContext.EBL_Migrations.Count();

                        var counts = _dbContext.EBL_Migrations
                            .GroupBy(m => 1)
                            .Select(g => new
                            {
                             TotalCount = g.Count(),
                            // TotalAOF = g.Count(m => m.M_DATA_CLASS == "AOF")
                            })
                            .FirstOrDefault();

                        var totalMAccount = _dbContext.EBL_Migrations
                                     .Select(m => m.M_PRODUCT_TYPE)
                                     .Distinct()
                                     .Count();

                        var totalDistinctMStatus = _dbContext.EBL_Migrations
                                     .Select(m => m.M_STATUS)
                                     .Distinct()
                                     .Count();

                        var totalMUser = _dbContext.EBL_Migrations
                                    .Select(m => m.M_USER)
                                    .Distinct()
                                    .Count();

                        dTO.TotalCR =  counts?.TotalCount ?? 0; // If TotalCount Is null Or blank Then it assign O
                        dTO.TotalAOF = totalMAccount; // If TotalAof is Null Or Blank Then assign 0

                        dTO.TotalStatus = totalDistinctMStatus;
                        dTO.TotalUser = totalMUser;

                        break;

                    case "2":
                        
                        var count = _dbContext._EBL_POCs
                           .GroupBy(m => 1)
                           .Select(g => new
                           {
                               TotalCount = g.Count(), // Count All row 
                              // TotalAOF = g.Count(m => m.DATA_CLASS == "AOF") // Count Total AOF 
                           })
                           .FirstOrDefault();

                        var totalAccount = _dbContext._EBL_POCs
                                     .Select(m => m.PRODUCT_TYPE)
                                     .Distinct()
                                     .Count();

                        var totalDistinctStatus = _dbContext._EBL_POCs
                                     .Select(m => m.STATUS)
                                     .Distinct()
                                     .Count();

                        var totalUser = _dbContext._EBL_POCs
                                    .Select(m => m.USER)
                                    .Distinct()
                                    .Count();

                        dTO.TotalCR = count?.TotalCount ?? 0; 
                        dTO.TotalAOF = totalAccount;
                        dTO.TotalStatus = totalDistinctStatus;
                        dTO.TotalUser = totalUser;

                        break;

                    default:
                        throw new ArgumentException("Invalid Cabinate_Id", nameof(Cabinate_Id));
                }
            }
            catch (Exception e)
            {               
                // Example: _logger.LogError(e, "An error occurred while counting records");
                throw; // Rethrow the exception to preserve the stack trace
            }
         
            return dTO;
        }

        public List<WorkSpaceUserDTO> Get_WS_User(string Dept)
        {
            List<WorkSpaceUserDTO> userDTOs = new List<WorkSpaceUserDTO>();
            switch (Dept)
            {
                case "1":

                     var enumerableData = _dbContext.EBL_Migrations
                       .Select(x => new WorkSpaceUserDTO
                        {
                           User = x.M_USER ?? "NUll"
                        }
                       )
                       .Distinct()
                       .ToList();

                    userDTOs = enumerableData.ToList();

                    break;

                case "2":

                    var enumerableData_Poc = _dbContext._EBL_POCs
                       .Select(x => new WorkSpaceUserDTO{
                           User = x.USER ?? "NUll"
                        }
                       )
                       .Distinct()
                       .ToList();

                    userDTOs = enumerableData_Poc.ToList();

                    break;
            }                        
            return userDTOs;
        }

        public List<WorkSpaceStatusDTO> Get_WS_Status(string Dept)
        {
            List<WorkSpaceStatusDTO> userDTOs = new List<WorkSpaceStatusDTO>();
            var sevenDaysAgo = DateTime.Now.AddDays(-7).Date;
            switch (Dept)
            {
                case "1":

                    var enumerableData = _dbContext.EBL_Migrations.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                      .Select(x => new WorkSpaceStatusDTO
                      {
                          Status = x.M_STATUS ?? "NUll"
                      }
                      )
                      .Distinct()
                      .ToList();

                    userDTOs = enumerableData.ToList();

                    break;

                case "2":

                    var enumerableData_Poc = _dbContext._EBL_POCs.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                       .Select(x => new WorkSpaceStatusDTO
                       {
                           Status = x.STATUS ?? "NUll"
                       }
                       )
                       .Distinct()
                       .ToList();

                    userDTOs = enumerableData_Poc.ToList();

                    break;
            }
            return userDTOs;
        }

        public List<WorkSpaceAccountTypeDTO> Get_WS_AccountType(string Dept)
        {
            List<WorkSpaceAccountTypeDTO> userDTOs = new List<WorkSpaceAccountTypeDTO>();

            var sevenDaysAgo = DateTime.Now.AddDays(-7).Date;


            switch (Dept)
            {
                case "1":

                    var enumerableData = _dbContext.EBL_Migrations.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                      .Select(x => new WorkSpaceAccountTypeDTO
                      {
                          // Account Type Means ProductType
                          AccountType = x.M_PRODUCT_TYPE ?? "NUll"
                      }
                      )
                      .Distinct()
                      .ToList();

                    userDTOs = enumerableData.ToList();

                    break;

                case "2":

                    var enumerableData_Poc = _dbContext._EBL_POCs.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                       .Select(x => new WorkSpaceAccountTypeDTO
                       {
                           //Account Type Means ProductType
                           AccountType = x.PRODUCT_TYPE ?? "NUll"
                       }
                       )
                       .Distinct()
                       .ToList();

                    userDTOs = enumerableData_Poc.ToList();

                    break;
            }
            return userDTOs;
        }

        public List<WorkSpaceTotalRecord> GetWorkStationRecord(string Dept)
        {
            List<WorkSpaceTotalRecord> records = new List<WorkSpaceTotalRecord>();

            var sevenDaysAgo = DateTime.Now.AddDays(-7).Date;

            switch (Dept)
            {
                 
                case "1":
                 
                    var enumerableData = _dbContext.EBL_Migrations.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                                                            .AsEnumerable() // Evaluate the query locally
                                                 
                                                            .Select(x => new WorkSpaceTotalRecord
                                                            {
                                                                DWDOCID = x.DWDOCID,
                                                                DATA_CLASS = x.M_DATA_CLASS,
                                                                ACCOUNT_NO = x.M_ACCOUNT_NO,                                                             
                                                                DocumentName = x.M_DOCUMENT_NAME,
                                                                ProductType = x.M_PRODUCT_TYPE,
                                                                CIF = x.M_CIF,
                                                                BranchCode = x.M_PRODUCT_BRANCH,                                                           
                                                                STATUS = x.M_STATUS,                                                  
                                                            })
                                                            .OrderByDescending(x => x.DWDOCID);

                    records = enumerableData.ToList();

                    break;
                case "2":
                    var enumerableDatappoc = _dbContext._EBL_POCs.Where(x => x.DWSTOREDATETIME.Date >= sevenDaysAgo)
                                                         .AsEnumerable() // Evaluate the query locally                                         
                                                         .Select(x => new WorkSpaceTotalRecord
                                                         {
                                                             DWDOCID = x.DWDOCID,
                                                             ACCOUNT_NO = x.ACCOUNT_NO,
                                                             DATA_CLASS = x.DATA_CLASS,
                                                             ProductType = x.PRODUCT_TYPE,
                                                             DocumentName = x.DOCUMENT_NAME,
                                                             CIF = x.CIF,
                                                             BranchCode = x.BRANCH_CODE,
                                                             STATUS = x.STATUS,
                                                         })
                                                         .OrderByDescending(x => x.DWDOCID);

                    records = enumerableDatappoc.ToList();
                    break;
            }
            return records;
          
        }
    }
}


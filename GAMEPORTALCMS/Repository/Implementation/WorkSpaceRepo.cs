using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics;
using GAMEPORTALCMS.Models.DTO;
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
                             TotalAOF = g.Count(m => m.M_DATA_CLASS == "AOF")
                            })
                            .FirstOrDefault();

                        dTO.TotalCR =  counts?.TotalCount ?? 0;
                        dTO.TotalAOF = counts?.TotalAOF ?? 0;

                        break;

                    case "2":
                        
                        var count = _dbContext._EBL_POCs
                           .GroupBy(m => 1)
                           .Select(g => new
                           {
                               TotalCount = g.Count(),
                               TotalAOF = g.Count(m => m.DATA_CLASS == "AOF")
                           })
                           .FirstOrDefault();

                        dTO.TotalCR = count?.TotalCount ?? 0; 
                        dTO.TotalAOF = count?.TotalAOF ?? 0;

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
    }
}

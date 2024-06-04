using GAMEPORTALCMS.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics;

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
    }
}

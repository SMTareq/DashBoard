using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EBL_MigrationDTO>> GetEBLMigrationData(string type)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();
            
            //gameInfos = await (from x in _dbContext.EBL_Migrations.AsNoTracking()

            //                   select new EBL_MigrationDTO
            //                   {  
            //                       DWDOCID = x.DWDOCID,
            //                       DWSTOREDATETIME = x.DWSTOREDATETIME,
            //                       ACCOUNT_NO = x.ACCOUNT_NO,
            //                       PRODUCT_TYPE = x.PRODUCT_TYPE,
            //                       STATUS = x.STATUS,
            //                       M_CREATED_DATE = x.M_CREATED_DATE,
            //                       DOCUMENT_TYPE = x.DOCUMENT_TYPE,

            //                   }).ToListAsync();




            var query = from x in _dbContext.EBL_Migrations.AsNoTracking()
                        select new EBL_MigrationDTO
                        {
                            DWDOCID = x.DWDOCID,
                            DWSTOREDATETIME = x.DWSTOREDATETIME,
                            ACCOUNT_NO = x.ACCOUNT_NO,
                            PRODUCT_TYPE = x.PRODUCT_TYPE,
                            STATUS = x.STATUS,
                            M_CREATED_DATE = x.M_CREATED_DATE,
                            DOCUMENT_TYPE = x.DOCUMENT_TYPE,
                        };

            //if (startDate.HasValue && endDate.HasValue)
            //{
            //    query = query.Where(x => x.M_CREATED_DATE >= startDate && x.M_CREATED_DATE <= endDate);
            //}

            //if (!string.IsNullOrEmpty(type))
            //{
            //    query = query.Where(x => x.STATUS == type);
            //}

            gameInfos = await query.ToListAsync();

            return gameInfos;
        }

    }
}

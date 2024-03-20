using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Xml.Linq;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EBL_MigrationDTO>> GetEBLMigrationData(string? Department, string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
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

            if(Department == "EBLMigration")
            {
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
               
                Func<EBL_MigrationDTO,bool> filter = x => x.DWDOCID != null;

                if (!string.IsNullOrEmpty(DocClass) && DocClass != "0")
                { 
                    filter = x => filter(x) && x.PRODUCT_TYPE == DocClass;
                }

                if (!string.IsNullOrEmpty(status) && status != "0")
                {
                    filter = x => filter(x) && x.STATUS == status;
                }

                if (FromDate != null && Todate != null)
                {            
                    filter = x => filter(x) && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate;
                }

              // query = query.Where(filter);
                
                gameInfos = await query.ToListAsync();

                //if (FromDate != null && Todate != null)
                //{
                //    query = query.Where(x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
                //}

            }


            return gameInfos;
        }




    }
}

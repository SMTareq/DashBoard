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

        public async Task<List<EBL_MigrationDTO>> GetEBLMigrationData(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();
                 
                var query = from x in _dbContext.EBL_Migrations.AsNoTracking()
                            select new EBL_MigrationDTO
                            {
                                DWDOCID = x.DWDOCID,
                                DWSTOREDATETIME = x.DWSTOREDATETIME,
                                ACCOUNT_NO = x.ACCOUNT_NO,
                                DATA_CLASS = x.DATA_CLASS,
                                PRODUCT_TYPE = x.PRODUCT_TYPE,
                                STATUS = x.STATUS,
                                M_CREATED_DATE = x.M_CREATED_DATE,
                                DOCUMENT_TYPE = x.DOCUMENT_TYPE,
                            };

            //Func<EBL_MigrationDTO,bool> filter = x => x.DWDOCID != null;

            //if (!string.IsNullOrEmpty(DocClass) && DocClass != "0")
            //{ 
            //    filter = x => filter(x) && x.PRODUCT_TYPE == DocClass;
            //}

            //if (!string.IsNullOrEmpty(status) && status != "0")
            //{
            //    filter = x => filter(x) && x.STATUS == status;
            //}

            //if (FromDate != null && Todate != null)
            //{            
            //    filter = x => filter(x) && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate;
            //}

            if (DocClass == "0")
            {
                DocClass = null;
            }
            if(status == "0")
            {
                status = null;
            }
            if (DocClass != null)
            {
                query = query.Where(x => x.PRODUCT_TYPE == DocClass);
            }
            if (status != null)
            {
                query = query.Where(x => x.STATUS == status);
            }          
            if (FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && status != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.STATUS == status);
            }
            if (DocClass != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (status != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.STATUS == status && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && status != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.STATUS == status && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }

            gameInfos = await query.ToListAsync();
                        
            return gameInfos;
        }

        public async Task<List<EBLPOCDTO>> GetEblPocData( string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            var query = from x in _dbContext._EBL_POCs.AsNoTracking()
                        select new EBLPOCDTO
                        {
                            DWDOCID = x.DWDOCID,
                            DWSTOREDATETIME_ = x.DWSTOREDATETIME.ToString("f"),
                            ACCOUNT_NO = x.ACCOUNT_NO,
                            PRODUCT_TYPE = x.PRODUCT_TYPE,
                            DATA_CLASS =x.DATA_CLASS,
                            STATUS = x.STATUS,
                            WF_CREATOR = x.WF_CREATOR,
                            WF_CREATOR_COMMENTS = x.WF_CREATOR_COMMENTS ,
                            WF_MAKER = x.WF_MAKER,
                            WF_MAKER_COMMENTS = x.WF_MAKER_COMMENTS,
                            WF_CHECKER  = x.WF_CHECKER,
                            WF_CHECKER_COMMENTS = x.WF_CHECKER_COMMENTS,
                            WF_MANAGER =x.WF_MANAGER,
                            WF_MANAGER_COMMENTS = x.WF_MANAGER
                         };

            if (DocClass == "0")
            {
                DocClass = null;
            }
            if (status == "0")
            {
                status = null;
            }
            if (DocClass != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass);
            }
            if (status != null )
            {
                query = query.Where(x => x.STATUS == status);
            }          
            if (DocClass != null && status != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass &&  x.STATUS == status);
            }
            if (FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null &&  FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (status != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.STATUS == status && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && status != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.STATUS == status && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            gameInfos = await query.ToListAsync();

            return gameInfos;
        }
  

        public async Task<List<EBLPOCDTO>> GetEblDataClassLoad(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();
        
            switch (DepartmentId)
            {
                case "2":
                    
                    var distinctDataClasses = await _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().ToListAsync();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        // Create a new EBLPOCDTO object for each distinct data class
                        EBLPOCDTO dto = new EBLPOCDTO();
                        dto.DATA_CLASS = dataClass;

                        // Add the DTO to the list
                        gameInfos.Add(dto);
                    }
                    break;
                case "1":
                    
                    var distinctDataClassesMigration = await _dbContext.EBL_Migrations.Select(p => p.DATA_CLASS).Distinct().ToListAsync();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        // Create a new EBLPOCDTO object for each distinct data class
                        EBLPOCDTO dto = new EBLPOCDTO();

                        if(!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                      
                        // Add the DTO to the list
                        
                    }
                    break;             
            }

            return gameInfos;
        }

    }


}

using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Net.Mail;

using System.Net;

//using System.Data.Entity;

using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Xml.Linq;
using iRely.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using GAMEPORTALCMS.Models.Entity;
using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;
using System.Linq;
using IdeaBlade.Linq;
using Twilio;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public   List<EBL_MigrationDTO> GetEBLMigrationData(string? AccountNo, string? status, DateTime? FromDate, DateTime? Todate, string? ProductBranch, string? ProductType, string? CIF)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();
         
            IEnumerable<EBL_MigrationDTO> enumerableData =  from x in _dbContext.EBL_Migrations.DefaultIfEmpty()
                                                           
                         orderby x.DWDOCID descending
                         select new EBL_MigrationDTO
                              {
                                  DWDOCID = x.DWDOCID,
                                  DATA_CLASS = x.M_DATA_CLASS,                
                                  ACCOUNT_NO = x.M_ACCOUNT_NO,
                                  DWSTOREDATETIME =x.DWSTOREDATETIME,
                                  DocumentName = x.M_DOCUMENT_NAME,
                                  ProductType = x.M_PRODUCT_TYPE,
                                  CIF = x.M_CIF,
                                  BranchCode = x.M_PRODUCT_BRANCH,

                                  //DWSTOREDATETIME_con = x.DWSTOREDATETIME.ToString("U"),
                                  STATUS = x.M_STATUS,
                                 // DATA_CLASS=x.DATA_CLASS
                              };

            // Convert to IQueryable
            IQueryable<EBL_MigrationDTO> queryableData = enumerableData.AsQueryable();

            // Initial perameter
            Func<EBL_MigrationDTO, bool> predicate = x => x.DWDOCID != null;

            if (AccountNo == "Select From List")
            {
                AccountNo = null;
            }
            if(status == "0")
            {
                status = null;
            }
            if (ProductBranch == "Select From List")
            {
                ProductBranch = null;
            }
            if (ProductType == "Select From List")
            {
                ProductType = null;
            }
            
            if (AccountNo != null)
            {
                predicate = CombinePredicates(predicate, x => x.ACCOUNT_NO == AccountNo);              
            }

            if (ProductBranch != null)
            {
                predicate = CombinePredicates(predicate, x => x.BranchCode == ProductBranch);
            }

            if (ProductType != null)
            {
                predicate = CombinePredicates(predicate, x => x.ProductType == ProductType);
            }

            if (CIF != "Select From List")
            {
                predicate = CombinePredicates(predicate, x => x.CIF == CIF);
            }

            if (status != null)
            {              
                predicate = CombinePredicates(predicate, x => x.STATUS == status);
            }

            if (FromDate != null && Todate != null)
            {        
                predicate = CombinePredicates(predicate, x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
       
            enumerableData = enumerableData.Where(predicate);

            gameInfos = enumerableData.ToList();
                        
            return gameInfos;
         
        }

        public List<EBLPOCDTO> GetEblProductTypeLoadSync(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            EBLPOCDTO dtoo = new EBLPOCDTO();
            dtoo.DATA_CLASS = "Select From List";
            gameInfos.Add(dtoo);

            switch (DepartmentId)
            {
                case "2":
                    var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.PRODUCT_TYPE).Distinct().ToList();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;

                case "1":
                    var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.M_PRODUCT_TYPE).Distinct().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;
            }
            return gameInfos;
        }

        public List<EBLPOCDTO> GetEblAccountNoLoadSync(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            EBLPOCDTO dtoo = new EBLPOCDTO();
            dtoo.DATA_CLASS = "Select From List";
            gameInfos.Add(dtoo);

            switch (DepartmentId)
            {
                case "2":
                    var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.ACCOUNT_NO).Distinct().ToList();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;

                case "1":
                    var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.M_ACCOUNT_NO).Distinct().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;
            }
            return gameInfos;
        }


        public List<EBLPOCDTO> GetEblBrachCodeLoadSync(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            EBLPOCDTO dtoo = new EBLPOCDTO();
            dtoo.DATA_CLASS = "Select From List";
            gameInfos.Add(dtoo);

            switch (DepartmentId)
            {
                case "2":
                    var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.BRANCH_CODE).Distinct().ToList();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;

                case "1":
                    var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.M_PRODUCT_BRANCH).Distinct().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;
            }
            return gameInfos;
        }


        public List<EBLPOCDTO> GetEblDataClassLoadSync(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            EBLPOCDTO dtoo = new EBLPOCDTO();
            dtoo.DATA_CLASS = "Select From List";
            gameInfos.Add(dtoo);

            switch (DepartmentId)
            {
                case "2":
                    var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().ToList();
            
                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;                         
                            gameInfos.Add(dto);
                        }                       
                    }
                    break;

                case "1":
                    var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.M_DATA_CLASS).Distinct().ToList();
                  
                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;
            }

            return gameInfos;
        }


        public List<EBLPOCDTO> GetEblCIFLoadSync(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            EBLPOCDTO dtoo = new EBLPOCDTO();
            dtoo.DATA_CLASS = "Select From List";
            gameInfos.Add(dtoo);

            switch (DepartmentId)
            {
                case "2":
                    var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.CIF).Distinct().ToList();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;

                case "1":
                    var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.M_CIF).Distinct().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBLPOCDTO dto = new EBLPOCDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.DATA_CLASS = dataClass;
                            gameInfos.Add(dto);
                        }
                    }
                    break;
            }

            return gameInfos;
        }

        //Status Populate 
        public List<EBL_MigrationDTO> GetEblStatusLoadSync(string? DepartmentId)
        {
            List<EBL_MigrationDTO> statusInfos = new List<EBL_MigrationDTO>();
    
            switch (DepartmentId)
            {
                case "1":
                    var distinctDataClasses = _dbContext.EBL_Migrations.Select(p => p.M_STATUS).Distinct().ToList();

                    foreach (var dataClass in distinctDataClasses)
                    {
                        EBL_MigrationDTO dto = new EBL_MigrationDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.STATUS = dataClass;
                            statusInfos.Add(dto);
                        }
                    }
                    break;

                case "2":
                    var distinctDataClassesMigration = _dbContext._EBL_POCs.Select(p => p.STATUS).Distinct().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        EBL_MigrationDTO dto = new EBL_MigrationDTO();
                        if (!string.IsNullOrEmpty(dataClass))
                        {
                            dto.STATUS = dataClass;
                            statusInfos.Add(dto);
                        }
                    }
                    break;
            }

            return statusInfos;
        }

        public delegate int MyDelegate(); //declaring a delegate

        public  List<EBLPOCDTO> GetEblPocData( string? AccountNo, string? status, DateTime? FromDate, DateTime? Todate, string? ProductBranch, string? ProductType, string? CIF)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();
       
            IEnumerable<EBLPOCDTO> enumerableData = from x in _dbContext._EBL_POCs.DefaultIfEmpty()
                                                    orderby x.DWDOCID descending
                                                    select new EBLPOCDTO
                                                    {
                                                        DWDOCID = x.DWDOCID,
                                                        DWSTOREDATETIME = x.DWSTOREDATETIME,
                                                        //DWSTOREDATETIME_ = x.DWSTOREDATETIME.ToString("f"),
                                                        ACCOUNT_NO = x.ACCOUNT_NO,
                                                        DATA_CLASS = x.DATA_CLASS,
                                                        PRODUCT_TYPE = x.PRODUCT_TYPE,
                                                        DOCUMENT_NAME = x.DOCUMENT_NAME,
                                                        CIF = x.CIF,
                                                        BranchCode = x.BRANCH_CODE,
                                                        STATUS = x.STATUS,
                                                    };
            // Convert to IQueryable
            IQueryable<EBLPOCDTO> queryableData = enumerableData.AsQueryable();

            Func<EBLPOCDTO, bool> predicate = x => x.DWDOCID != null;

            if (AccountNo == "Select From List")
            {
                AccountNo = null;
            }
            if (status == "0")
            {
                status = null;
            }
            if (ProductBranch == "Select From List")
            {
                ProductBranch = null;
            }

            if (ProductType == "Select From List")
            {
                ProductType = null;
            }
       
            if (AccountNo != null)
            {     
                predicate = CombinePredicates(predicate, x => x.ACCOUNT_NO == AccountNo);
            }

            if (ProductBranch != null)
            {
                predicate = CombinePredicates(predicate, x => x.BranchCode == ProductBranch);
            }

            if (ProductType != null)
            {
                predicate = CombinePredicates(predicate, x => x.PRODUCT_TYPE == ProductType);
            }
            if (CIF != "Select From List")
            {
                predicate = CombinePredicates(predicate, x => x.CIF == CIF);
            }

            if (status != null)
            {              
                predicate = CombinePredicates(predicate, x => x.STATUS == status);
            }
            
            if (FromDate != null && Todate != null)
            {
                predicate = CombinePredicates(predicate, x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);   
            }

            enumerableData = enumerableData.Where(predicate);

            gameInfos = enumerableData.ToList();

            return gameInfos;
        }

        public static Func<T, bool> CombinePredicates<T>(Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return x => predicate1(x) && predicate2(x);
        }

        //public Task<List<EBLPOCDTO>> GetEblDataClassLoad(string? DepartmentId)
        //{
        //    List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

        //    switch (DepartmentId)
        //    {
        //        case "2":

        //           // var distinctDataClasses = await _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().ToListAsync();

        //            var distinctDataClasses = _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().AsEnumerable().ToList();


        //            foreach (var dataClass in distinctDataClasses)
        //            {
        //                // Create a new EBLPOCDTO object for each distinct data class
        //                EBLPOCDTO dto = new EBLPOCDTO();
        //                dto.DATA_CLASS = dataClass;

        //                // Add the DTO to the list
        //                gameInfos.Add(dto);
        //            }
        //            break;
        //        case "1":

        //            var distinctDataClassesMigration = _dbContext.EBL_Migrations.Select(p => p.DATA_CLASS).Distinct().AsEnumerable().ToList();

        //            //var distinctDataClassesMigration = await _dbContext.EBL_Migrations.Select(p => p.DATA_CLASS).Distinct().ToListAsync();

        //            foreach (var dataClass in distinctDataClassesMigration)
        //            {
        //                // Create a new EBLPOCDTO object for each distinct data class
        //                EBLPOCDTO dto = new EBLPOCDTO();

        //                if(!string.IsNullOrEmpty(dataClass))
        //                {
        //                    dto.DATA_CLASS = dataClass;
        //                    gameInfos.Add(dto);
        //                }

        //                // Add the DTO to the list

        //            }
        //            break;             
        //    }

        //    return gameInfos;
        //}

        public async Task<List<EBLPOCDTO>> GetEblDataClassLoada(string? DepartmentId)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            switch (DepartmentId)
            {
                case "2":

                   // var distinctDataClasses = await _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().ToListAsync();


                    var distinctDataClasses =  _dbContext._EBL_POCs.Select(p => p.DATA_CLASS).Distinct().AsEnumerable().ToList();

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

                   // var distinctDataClassesMigration = await _dbContext.EBL_Migrations.Select(p => p.DATA_CLASS).Distinct().ToListAsync();

                    var distinctDataClassesMigration =  _dbContext.EBL_Migrations.Select(p => p.DATA_CLASS).Distinct().AsEnumerable().ToList();

                    foreach (var dataClass in distinctDataClassesMigration)
                    {
                        // Create a new EBLPOCDTO object for each distinct data class
                        EBLPOCDTO dto = new EBLPOCDTO();

                        if (!string.IsNullOrEmpty(dataClass))
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

        #region PIEChart

        public async Task<List<PieChartDTO>> GetPieList(string type)
        {
            List<PieChartDTO> gameInfos = new List<PieChartDTO>();
            if (type == "Status")
            {
                // Retrieve all DownloadableGames and GameCategories
                var EbLMigration = await _dbContext.EBL_Migrations.ToListAsync();

                //  Perform left join in-memory
                var result = from e in EbLMigration
                             where e.STATUS != null
                             group e by e.STATUS into g
                             select new PieChartDTO
                             {
                                 CategoryName = g.Key,
                                 Total = g.Count()
                             };

                gameInfos = result.ToList();

                Dictionary<string?, int> pivotTable = result
                   .GroupBy(x => x.CategoryName)
                   .ToDictionary(
                    group => group.Key,
                    group => group.Sum(item => item.Total)
                     );
            }
      
            return gameInfos;
        }

        public List<PieChartDTO> GetPieListSync(string type, DateTime? fromdate, DateTime? todate)
        {
            List<PieChartDTO> gameInfos = new List<PieChartDTO>();

            if (type == "Status")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_STATUS != null
                             group e by e.M_STATUS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();             
            }

            if (type == "DOCClass")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DATA_CLASS != null
                             group e by e.M_DATA_CLASS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();
            
            }

            if (type == "MCIF")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_CIF != null
                             group e by e.M_CIF into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();
       
            }

            //New
            if (type == "MDOCUMENTNAME")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DOCUMENT_NAME != null
                             group e by e.M_DOCUMENT_NAME into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MOWNER")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_OWNER != null
                             group e by e.M_OWNER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_TYPE != null
                             group e by e.M_PRODUCT_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MUser")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_USER != null
                             group e by e.M_USER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MTYPE")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_TYPE != null
                             group e by e.M_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_BRANCH != null
                             group e by e.M_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            //END

            if (type == "Status" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_STATUS != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_STATUS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }

            if (type == "DOCClass" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DATA_CLASS != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_DATA_CLASS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }

            if (type == "MCIF" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_CIF != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_CIF into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }

            //New
            if (type == "MDOCUMENTNAME" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DOCUMENT_NAME != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_DOCUMENT_NAME into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }

            if (type == "MOWNER" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_OWNER != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_OWNER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_TYPE != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_PRODUCT_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MUser" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_USER != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_USER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MTYPE" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_TYPE != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext.EBL_Migrations.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_BRANCH != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.M_PRODUCT_BRANCH into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();
            }

            return gameInfos;
        }


        //EBL_POC Pie Chart
        public List<PieChartDTO> GetPieChart_EBl_POCList_Sync(string type, DateTime? fromdate, DateTime? todate)
        {
            List<PieChartDTO> gameInfos = new List<PieChartDTO>();
            if (type == "Status")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.STATUS != null
                             group e by e.STATUS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }
            if (type == "DOCClass")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DATA_CLASS != null
                             group e by e.DATA_CLASS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "MCIF")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.CIF != null
                             group e by e.CIF into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }

            //NEW
            if (type == "DOCUMENTNAME")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DOCUMENT_NAME != null
                             group e by e.DOCUMENT_NAME into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTTYPE")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.PRODUCT_TYPE != null
                             group e by e.PRODUCT_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "USER")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.USER != null
                             group e by e.USER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "PRODUCTBranch")
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.BRANCH_CODE != null
                             group e by e.BRANCH_CODE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            //END

            if (type == "Status" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.STATUS != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.STATUS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }
            if (type == "DOCClass" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DATA_CLASS != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.DATA_CLASS into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "MCIF" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.CIF != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.CIF into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "DOCUMENTNAME" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DOCUMENT_NAME != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.DOCUMENT_NAME into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTTYPE" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.PRODUCT_TYPE != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.PRODUCT_TYPE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "USER" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.USER != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.USER into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            if (type == "PRODUCTBranch" && fromdate != null && todate != null)
            {
                // Retrieve all DownloadableGames and GameCategories synchronously
                var EbLMigration = _dbContext._EBL_POCs.ToList();

                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.BRANCH_CODE != null && e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate
                             group e by e.BRANCH_CODE into g
                             select new PieChartDTO
                             {
                                 name = g.Key,
                                 y = g.Count()
                             };
                gameInfos = result.ToList();

            }
            return gameInfos;
        }


        #endregion

        #region BARCHART

        public  List<BarChart> GetEblMigration_BarChart_List(string type, DateTime? FromDate, DateTime? Todate)
        {
            List<BarChart> gameInfos = new List<BarChart>();

            if (type == "Status")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_STATUS != null
                             group e by e.M_STATUS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "DOCClass")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DATA_CLASS != null
                             group e by e.M_DATA_CLASS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MCIF")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_CIF != null
                             group e by e.M_CIF into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            //New
            if (type == "MDOCUMENTNAME")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DOCUMENT_NAME != null
                             group e by e.M_DOCUMENT_NAME into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MOWNER")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_OWNER != null
                             group e by e.M_OWNER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MTYPE")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_TYPE != null
                             group e by e.M_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_TYPE != null
                             group e by e.M_PRODUCT_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MUser")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_USER != null
                             group e by e.M_USER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch")
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_BRANCH != null
                             group e by e.M_PRODUCT_BRANCH into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            //End

            if (type == "Status" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_STATUS != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_STATUS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "DOCClass" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DATA_CLASS != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_DATA_CLASS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MCIF" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_CIF != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_CIF into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }


            //New

            if (type == "MDOCUMENTNAME" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_DOCUMENT_NAME != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_DOCUMENT_NAME into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MOWNER" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_OWNER != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_OWNER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MTYPE" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_TYPE != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_TYPE != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_PRODUCT_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MUser" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_USER != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_USER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext.EBL_Migrations.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.M_PRODUCT_BRANCH != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.M_PRODUCT_BRANCH into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            return gameInfos;
        }

        //Graph for Ebl_Poc
        public List<BarChart> GetEblPOC_BarChart_List(string type, DateTime? FromDate, DateTime? Todate)
        {
            List<BarChart> gameInfos = new List<BarChart>();

            if (type == "Status")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.STATUS != null 
                             group e by e.STATUS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "DOCClass")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DATA_CLASS != null
                             group e by e.DATA_CLASS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MCIF")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.CIF != null
                             group e by e.CIF into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            //New

            if (type == "DOCUMENTNAME")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DOCUMENT_NAME != null
                             group e by e.DOCUMENT_NAME into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "PRODUCTTYPE")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.PRODUCT_TYPE != null
                             group e by e.PRODUCT_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "USER")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.USER != null
                             group e by e.USER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "PRODUCTBranch")
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.BRANCH_CODE != null
                             group e by e.BRANCH_CODE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            //End

            if (type == "Status" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.STATUS != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.STATUS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "DOCClass" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DATA_CLASS != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.DATA_CLASS into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "MCIF" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.CIF != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.CIF into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "DOCUMENTNAME" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.DOCUMENT_NAME != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.DOCUMENT_NAME into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "PRODUCTTYPE" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.PRODUCT_TYPE != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.PRODUCT_TYPE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "USER" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.USER != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.USER into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            if (type == "PRODUCTBranch" && FromDate != null && Todate != null)
            {
                var EbLMigration = _dbContext._EBL_POCs.ToList();
                // Perform left join in-memory
                var result = from e in EbLMigration
                             where e.BRANCH_CODE != null && e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate
                             group e by e.BRANCH_CODE into g
                             select new BarChart
                             {
                                 Name = g.Key,
                                 data = new int[] { g.Count() }
                             };
                gameInfos = result.ToList();
            }

            return gameInfos;
        }

        #endregion

    }

    //internal class T
    //{
    //}
}

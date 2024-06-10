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
using System.Net.NetworkInformation;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
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
       
            Func<_EBL_POC, bool> predicate = x => x.DWDOCID != null;

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
                predicate = CombinePredicates(predicate, x => x.BRANCH_CODE == ProductBranch);
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
            //

            var enumerableData = _dbContext._EBL_POCs
                                                         .AsEnumerable() // Evaluate the query locally
                                                         .Where(predicate)
                                                         .Select(x => new EBLPOCDTO
                                                         {
                                                             DWDOCID = x.DWDOCID,
                                                             DWSTOREDATETIME = x.DWSTOREDATETIME,
                                                             
                                                             ACCOUNT_NO = x.ACCOUNT_NO,
                                                             DATA_CLASS = x.DATA_CLASS,
                                                             ProductType = x.PRODUCT_TYPE,
                                                             DocumentName = x.DOCUMENT_NAME,
                                                             CIF = x.CIF,
                                                             BranchCode = x.BRANCH_CODE,
                                                             STATUS = x.STATUS,
                                                             
                                                         })
                                                         .OrderByDescending(x => x.DWDOCID);

            gameInfos = enumerableData.ToList();

            return gameInfos;

            //
         
        }

        public static Func<T, bool> CombinePredicates<T>(Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return x => predicate1(x) && predicate2(x);
        }

      

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
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_STATUS) ? "Null" : e.M_STATUS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y =  g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();             
            }

            if (type == "DOCClass")
            {               
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_DATA_CLASS) ? "Null" : e.M_DATA_CLASS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();
            
            }

            if (type == "MCIF")
            {               
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_CIF) ? "Null" : e.M_CIF)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
       
            }

            //New
            if (type == "MDOCUMENTNAME")
            {             
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_DOCUMENT_NAME) ? "Null" : e.M_DOCUMENT_NAME)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MOWNER")
            {             
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_OWNER) ? "Null" : e.M_OWNER)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE")
            {             
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_TYPE) ? "Null" : e.M_PRODUCT_TYPE)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MUser")
            {             
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_USER) ? "Null" : e.M_USER)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MTYPE")
            {               
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_TYPE) ? "Null" : e.M_TYPE)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch")
            {               
                var result = _dbContext.EBL_Migrations
               .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_BRANCH) ? "Null" : e.M_PRODUCT_BRANCH)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            //END

            if (type == "Status" && fromdate != null && todate != null)
            {               
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_STATUS) ? "Null" : e.M_STATUS)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();

            }

            if (type == "DOCClass" && fromdate != null && todate != null)
            {            
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_DATA_CLASS) ? "Null" : e.M_DATA_CLASS)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();

            }

            if (type == "MCIF" && fromdate != null && todate != null)
            {              
               var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_CIF) ? "Null" : e.M_CIF)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();
                gameInfos = result.ToList();
            }

            //New
            if (type == "MDOCUMENTNAME" && fromdate != null && todate != null)
            {           
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_DOCUMENT_NAME) ? "Null" : e.M_DOCUMENT_NAME)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();

            }

            if (type == "MOWNER" && fromdate != null && todate != null)
            {             
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_OWNER) ? "Null" : e.M_OWNER)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE" && fromdate != null && todate != null)
            {            
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_TYPE) ? "Null" : e.M_PRODUCT_TYPE)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MUser" && fromdate != null && todate != null)
            {              
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_USER) ? "Null" : e.M_USER)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MTYPE" && fromdate != null && todate != null)
            {             
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_TYPE) ? "Null" : e.M_TYPE)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch" && fromdate != null && todate != null)
            {             
                var result = _dbContext.EBL_Migrations
               .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
               .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_BRANCH) ? "Null" : e.M_PRODUCT_BRANCH)
               .Select(g => new PieChartDTO
               {
                   name = g.Key,
                   y = g.Count(),
               })
               .ToList();

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
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.STATUS) ? "Null" : e.STATUS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();
                gameInfos = result.ToList();

            }
            if (type == "DOCClass")
            {                        
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.DATA_CLASS) ? "Null" : e.DATA_CLASS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "MCIF")
            {              
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.CIF) ? "Null" : e.CIF)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }

            //NEW
            if (type == "DOCUMENTNAME")
            {            
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.DOCUMENT_NAME) ? "Null" : e.DOCUMENT_NAME)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTTYPE")
            {              
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.PRODUCT_TYPE) ? "Null" : e.PRODUCT_TYPE)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "USER")
            {             
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.USER) ? "Null" : e.USER)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTBranch")
            {                          
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.BRANCH_CODE) ? "Null" : e.BRANCH_CODE)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            //END

            if (type == "Status" && fromdate != null && todate != null)
            {
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.STATUS) ? "Null" : e.STATUS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "DOCClass" && fromdate != null && todate != null)
            {             
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.DATA_CLASS) ? "Null" : e.DATA_CLASS)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "MCIF" && fromdate != null && todate != null)
            {
                
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.CIF) ? "Null" : e.CIF)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "DOCUMENTNAME" && fromdate != null && todate != null)
            {
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.DOCUMENT_NAME) ? "Null" : e.DOCUMENT_NAME)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTTYPE" && fromdate != null && todate != null)
            {
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.PRODUCT_TYPE) ? "Null" : e.PRODUCT_TYPE)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "USER" && fromdate != null && todate != null)
            {
             
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.USER) ? "Null" : e.USER)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

                gameInfos = result.ToList();

            }
            if (type == "PRODUCTBranch" && fromdate != null && todate != null)
            {             
                var result = _dbContext._EBL_POCs
                .Where(e => e.DWSTOREDATETIME >= fromdate && e.DWSTOREDATETIME <= todate)
                .GroupBy(e => string.IsNullOrEmpty(e.BRANCH_CODE) ? "Null" : e.BRANCH_CODE)
                .Select(g => new PieChartDTO
                {
                    name = g.Key,
                    y = g.Count(),
                })
                .ToList();

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
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_STATUS) ? "Null" : e.M_STATUS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();          
                gameInfos = result.ToList();
            }

            if (type == "DOCClass")
            {                         
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_DATA_CLASS) ? "Null" : e.M_DATA_CLASS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MCIF")
            {                            
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_CIF) ? "Null" : e.M_CIF)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            //New
            if (type == "MDOCUMENTNAME")
            {
            
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_DOCUMENT_NAME) ? "Null" : e.M_DOCUMENT_NAME)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MOWNER")
            {               
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_OWNER) ? "Null" : e.M_OWNER)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MTYPE")
            {            
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_TYPE) ? "Null" : e.M_TYPE)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE")
            {            
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_TYPE) ? "Null" : e.M_PRODUCT_TYPE)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MUser")
            {         
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_USER) ? "Null" : e.M_USER)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch")
            {             
                var result = _dbContext.EBL_Migrations
                .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_BRANCH) ? "Null" : e.M_PRODUCT_BRANCH)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            //End

            if (type == "Status" && FromDate != null && Todate != null)
            {               
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_STATUS) ? "Null" : e.M_STATUS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            if (type == "DOCClass" && FromDate != null && Todate != null)
            {               
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_DATA_CLASS) ? "Null" : e.M_DATA_CLASS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MCIF" && FromDate != null && Todate != null)
            {              
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_CIF) ? "Null" : e.M_CIF)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }


            //New

            if (type == "MDOCUMENTNAME" && FromDate != null && Todate != null)
            {              
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_DOCUMENT_NAME) ? "Null" : e.M_DOCUMENT_NAME)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MOWNER" && FromDate != null && Todate != null)
            {              
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_OWNER) ? "Null" : e.M_OWNER)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();            
                gameInfos = result.ToList();
            }

            if (type == "MTYPE" && FromDate != null && Todate != null)
            {               
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_TYPE) ? "Null" : e.M_TYPE)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTTYPE" && FromDate != null && Todate != null)
            {              
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_TYPE) ? "Null" : e.M_PRODUCT_TYPE)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();
                gameInfos = result.ToList();
            }

            if (type == "MUser" && FromDate != null && Todate != null)
            {           
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_USER) ? "Null" : e.M_USER)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MPRODUCTBranch" && FromDate != null && Todate != null)
            {              
                var result = _dbContext.EBL_Migrations
                .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
                .GroupBy(e => string.IsNullOrEmpty(e.M_PRODUCT_BRANCH) ? "Null" : e.M_PRODUCT_BRANCH)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

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
                
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.STATUS) ? "Null" : e.STATUS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            if (type == "DOCClass")
            {              
                var result = _dbContext._EBL_POCs
                .GroupBy(e => string.IsNullOrEmpty(e.DATA_CLASS) ? "Null" : e.DATA_CLASS)
                .Select(g => new BarChart
                {
                    Name = g.Key,
                    data = new int[] { g.Count() }
                })
                .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MCIF")
            {             
                var result = _dbContext._EBL_POCs
               .GroupBy(e => string.IsNullOrEmpty(e.CIF) ? "Null" : e.CIF)
               .Select(g => new BarChart
               {
                   Name = g.Key,
                   data = new int[] { g.Count() }
               })
               .ToList();


                gameInfos = result.ToList();
            }

            //New

            if (type == "DOCUMENTNAME")
            {           
                var result = _dbContext._EBL_POCs
               .GroupBy(e => string.IsNullOrEmpty(e.DOCUMENT_NAME) ? "Null" : e.DOCUMENT_NAME)
               .Select(g => new BarChart
               {
                   Name = g.Key,
                   data = new int[] { g.Count() }
               })
               .ToList();

                gameInfos = result.ToList();
            }

            if (type == "PRODUCTTYPE")
            {            
                var result = _dbContext._EBL_POCs
               .GroupBy(e => string.IsNullOrEmpty(e.PRODUCT_TYPE) ? "Null" : e.PRODUCT_TYPE)
               .Select(g => new BarChart
               {
                   Name = g.Key,
                   data = new int[] { g.Count() }
               })
               .ToList();


                gameInfos = result.ToList();
            }

            if (type == "USER")
            {             
               var result = _dbContext._EBL_POCs
              .GroupBy(e => string.IsNullOrEmpty(e.USER) ? "Null" : e.USER)
              .Select(g => new BarChart
              {
                  Name = g.Key,
                  data = new int[] { g.Count() }
              })
              .ToList();
               gameInfos = result.ToList();
            }

            if (type == "PRODUCTBranch")
            {            
               var result = _dbContext._EBL_POCs
              .GroupBy(e => string.IsNullOrEmpty(e.BRANCH_CODE) ? "Null" : e.BRANCH_CODE)
              .Select(g => new BarChart
              {
                  Name = g.Key,
                  data = new int[] { g.Count() }
              })
              .ToList();

                gameInfos = result.ToList();
            }

            //End

            if (type == "Status" && FromDate != null && Todate != null)
            {
               
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.STATUS) ? "Null" : e.STATUS)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            if (type == "DOCClass" && FromDate != null && Todate != null)
            {                            
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.DATA_CLASS) ? "Null" : e.DATA_CLASS)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            if (type == "MCIF" && FromDate != null && Todate != null)
            {             
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.CIF) ? "Null" : e.CIF)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            if (type == "DOCUMENTNAME" && FromDate != null && Todate != null)
            {             
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.DOCUMENT_NAME) ? "Null" : e.DOCUMENT_NAME)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            if (type == "PRODUCTTYPE" && FromDate != null && Todate != null)
            {
               
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.PRODUCT_TYPE) ? "Null" : e.PRODUCT_TYPE)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            if (type == "USER" && FromDate != null && Todate != null)
            {
               
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.USER) ? "Null" : e.USER)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

             gameInfos = result.ToList();
            }

            if (type == "PRODUCTBranch" && FromDate != null && Todate != null)
            {              
              var result = _dbContext._EBL_POCs
             .Where(e => e.DWSTOREDATETIME >= FromDate && e.DWSTOREDATETIME <= Todate)
             .GroupBy(e => string.IsNullOrEmpty(e.BRANCH_CODE) ? "Null" : e.BRANCH_CODE)
             .Select(g => new BarChart
             {
                 Name = g.Key,
                 data = new int[] { g.Count() }
             })
             .ToList();

                gameInfos = result.ToList();
            }

            return gameInfos;
        }

        #endregion

        public List<EBL_MigrationDTO> GetEBLMigrationData(string? AccountNo, string? status, DateTime? FromDate, DateTime? Todate, string? ProductBranch, string? ProductType, string? CIF)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();
  
            Func<EBL_Migration, bool> predicate = x => x.DWDOCID != null;

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
                predicate = CombinePredicates(predicate, x => x.M_ACCOUNT_NO == AccountNo);
            }

            if (ProductBranch != null)
            {
                predicate = CombinePredicates(predicate, x => x.M_PRODUCT_BRANCH == ProductBranch);
            }

            if (ProductType != null)
            {
                predicate = CombinePredicates(predicate, x => x.M_PRODUCT_TYPE == ProductType);
            }

            if (CIF != "Select From List")
            {
                predicate = CombinePredicates(predicate, x => x.M_CIF == CIF);
            }

            if (status != null)
            {
                predicate = CombinePredicates(predicate, x => x.STATUS == status);
            }

            if (FromDate != null && Todate != null)
            {
                predicate = CombinePredicates(predicate, x => x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }

           // enumerableData = enumerableData.Where(predicate);

            var enumerableData =  _dbContext.EBL_Migrations                                                          
                                                            .AsEnumerable() // Evaluate the query locally
                                                            .Where(predicate)
                                                            .Select (x=> new EBL_MigrationDTO
                                                            {
                                                                DWDOCID = x.DWDOCID,
                                                                DATA_CLASS = x.M_DATA_CLASS,
                                                                ACCOUNT_NO = x.M_ACCOUNT_NO,
                                                                DWSTOREDATETIME = x.DWSTOREDATETIME,
                                                                DocumentName = x.M_DOCUMENT_NAME,
                                                                ProductType = x.M_PRODUCT_TYPE,
                                                                CIF = x.M_CIF,
                                                                BranchCode = x.M_PRODUCT_BRANCH,
                                                                //DWSTOREDATETIME_con = x.DWSTOREDATETIME.ToString("U"),
                                                                STATUS = x.M_STATUS,
                                                                // DATA_CLASS=x.DATA_CLASS
                                                            })
                                                            .OrderByDescending(x => x.DWDOCID);
 



            gameInfos = enumerableData.ToList();

            return gameInfos;

        }
    }

    //internal class T
    //{
    //}
}

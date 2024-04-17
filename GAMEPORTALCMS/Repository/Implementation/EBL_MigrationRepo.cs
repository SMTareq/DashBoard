﻿using GAMEPORTALCMS.Common;
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

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public   List<EBL_MigrationDTO> GetEBLMigrationData(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();

            var query =  from x in _dbContext.EBL_Migrations.DefaultIfEmpty()
                         orderby x.DWDOCID descending
                         select new EBL_MigrationDTO
                              {
                                  DWDOCID = x.DWDOCID,
                                  DATA_CLASS = x.M_DATA_CLASS,                
                                  ACCOUNT_NO = x.M_ACCOUNT_NO,
                                  DWSTOREDATETIME =x.DWSTOREDATETIME,
                                  //DWSTOREDATETIME_con = x.DWSTOREDATETIME.ToString("U"),
                                  STATUS = x.M_STATUS,
                                 // DATA_CLASS=x.DATA_CLASS
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

            if (DocClass == "Select From List")
            {
                DocClass = null;
            }
            if(status == "0")
            {
                status = null;
            }
            if (DocClass != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass);
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
                query = query.Where(x => x.DATA_CLASS == status && x.DWSTOREDATETIME >= FromDate && x.DWSTOREDATETIME <= Todate);
            }
            if (DocClass != null && status != null && FromDate != null && Todate != null)
            {
                query = query.Where(x => x.DATA_CLASS == DocClass && x.STATUS == status && x.DWSTOREDATETIME >=  FromDate && x.DWSTOREDATETIME <= Todate);
            }
            gameInfos = query.ToList();
                        
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

        public  List<EBLPOCDTO> GetEblPocData( string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            List<EBLPOCDTO> gameInfos = new List<EBLPOCDTO>();

            var query = from x in _dbContext._EBL_POCs.DefaultIfEmpty()
                        orderby x.DWDOCID descending
                        select new EBLPOCDTO
                        {
                            DWDOCID = x.DWDOCID,
                            DWSTOREDATETIME = x.DWSTOREDATETIME,
                           // DWSTOREDATETIME_ = x.DWSTOREDATETIME.ToString("f"),
                            ACCOUNT_NO = x.ACCOUNT_NO,                       
                            DATA_CLASS =x.DATA_CLASS,
                            STATUS = x.STATUS,
                          
                         };

            if (DocClass == "Select From List")
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
            gameInfos =  query.ToList();

            return gameInfos;
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

            if (type == "Status" && FromDate !=null  && Todate !=null)
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

            return gameInfos;
        }

        #endregion



    }


}

using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBL_MigrationController : ControllerBase
    {
   
        private readonly EBL_MigrationRepo eBL_Migration;

        public EBL_MigrationController(EBL_MigrationRepo bL_MigrationRepo, AppDBContext dbContext)
        {
            eBL_Migration = bL_MigrationRepo; 
        }

        [HttpGet("MigrationList")]
        public async Task<IActionResult> GetEBL_MigrationList(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            try
            {
                var data = await eBL_Migration.GetEBLMigrationData(DocClass, status, FromDate, Todate);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("EBLPOCList")]
        public async Task<IActionResult> GetEBL_POCList(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            try
            {
                var data = await eBL_Migration.GetEblPocData(DocClass, status, FromDate, Todate);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpGet("EblDataClassPopulate")]
        //public async Task<IActionResult> GetEBL_DataClassPopulateList(string? DepartmentId)
        //{
        //    try
        //    {
        //        var data = await eBL_Migration.GetEblDataClassLoad(DepartmentId);
        //        return Ok(data);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


        [HttpGet("EblDataClassPopulate")]
        public IActionResult GetEBL_DataClassPopulateList(string? DepartmentId)
        {
            try
            {
                var data = eBL_Migration.GetEblDataClassLoadSync(DepartmentId); // Call synchronous method
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //[HttpGet("PIEChart")]
        //public async Task<ActionResult<Dictionary<string, int>>> _Get_Ebl_Migration_Pie_Data(string type)
        //{
        //    try
        //    {
        //        var data = await eBL_Migration.GetPieList(type);
        //        return Ok(data);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpGet("PIEChart")]
        public ActionResult<Dictionary<string, int>> _Get_Ebl_Migration_Pie_Data(string Department,string type)
        {
            try
            {
                if (Department == "1")
                {
                    var data = eBL_Migration.GetPieListSync(type);
                    return Ok(data);
                }
                else
                {
                    var data = eBL_Migration.GetPieListSync(type);
                    return Ok(data);
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("BarChartForDashBoard")]
        public async Task<IActionResult> GetBarChartData(string Department, string type)
        {
            try
            {
                if (Department == "1")
                {
                    var data = eBL_Migration.GetEblMigration_BarChart_List(type);
                    return Ok(data);
                }
                else
                {
                    var data = eBL_Migration.GetPieListSync(type);
                    return Ok(data);
                }
            }
            catch ( Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }
    }
}

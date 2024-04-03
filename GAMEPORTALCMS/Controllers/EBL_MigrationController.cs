﻿using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using iRely.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBL_MigrationController : ControllerBase
    {
        private readonly EBL_MigrationRepo eBL_Migration;

        private readonly MailGenerator _mail;

        public EBL_MigrationController(EBL_MigrationRepo bL_MigrationRepo, AppDBContext dbContext, MailGenerator mail)
        {
            eBL_Migration = bL_MigrationRepo;
            _mail = mail;
        }

        [HttpGet("MigrationList")]
        public  IActionResult GetEBL_MigrationList(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            try
            {
                var data =  eBL_Migration.GetEBLMigrationData(DocClass, status, FromDate, Todate);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("EBLPOCList")]
        public  IActionResult GetEBL_POCList(string? DocClass, string? status, DateTime? FromDate, DateTime? Todate)
        {
            try
            {
                var data =  eBL_Migration.GetEblPocData(DocClass, status, FromDate, Todate);
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

        [HttpGet("PIEChart")]
        public ActionResult<Dictionary<string, int>> _Get_Ebl_Migration_Pie_Data(string Department, string type)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //Mail_Generator

        [HttpGet("MailGenerator")]
        public async Task<IActionResult> MailGenerator(string mailAddress)
        {
            bool data = await _mail.SendMail(mailAddress);
            if (data)
            {
                return new JsonResult(new ResponseModel { Success = true, Message = "Mail sent successfully" });
            }
            else
            {
                return new JsonResult(new ResponseModel { Success = false, Message = "error" });
            }
        }
    }
}

using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using GAMEPORTALCMS.Models.DTO;

namespace GAMEPORTALCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceController : ControllerBase
    {

        private readonly WorkSpaceRepo workspace;

        public WorkSpaceController(WorkSpaceRepo _workSpace)
        {
            workspace = _workSpace;
        }

        //[HttpGet("Total_Record_Of_Per_Cabinate")]
        //public async Task<IActionResult> GetTotalRecordCountAsync(string? Cabinate_Id)
        //{
        //    try
        //    {
        //        var totalCount = await workspace.TotalRecordOfPerCabinateAsync(Cabinate_Id);
        //        return Ok(totalCount);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpGet("Total_Record_Of_Per_Cabinate")]
        public IActionResult GetTotalRecordCount(string? Cabinate_Id)
        {
            try
            {
                DashBoardBanarDTO totalCount = new DashBoardBanarDTO();
                totalCount =  workspace.TotalRecordOfPerCabinate(Cabinate_Id);
                return Ok(totalCount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("WorkSpaceBennerUser")]
        public IActionResult GetWS_User(string? Dept)
        {
            try
            {
                var UserData = workspace.Get_WS_User(Dept);
                return Ok(UserData);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("WorkSpaceBennerStatus")]
        public IActionResult GetWS_Status(string? Dept)
        {
            try
            {
                var data = workspace.Get_WS_Status(Dept);

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("WorkSpacebennerAccountType")]
        public IActionResult GetWS_AccountType(string? Dept)
        {
            try
            {
                var data = workspace.Get_WS_AccountType(Dept);

                return Ok(data);

            }catch(Exception e)
            {
                return BadRequest(e.Message);   
            }
        }

        [HttpGet("WorkSpaceTotalRecord")]
        public IActionResult GetWS_A(string? Dept)
        {
            try
            {
                var data = workspace.GetWorkStationRecord(Dept);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}

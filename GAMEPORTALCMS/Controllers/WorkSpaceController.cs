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
    }
}

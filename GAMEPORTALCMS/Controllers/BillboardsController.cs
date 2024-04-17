using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BillboardsController : ControllerBase
    {

        GameCategoryService _categoryService;
        private readonly AppDBContext _dbContext;

        GameRepository _gameRepo;

        public BillboardsController(GameCategoryService categoryService, AppDBContext dbContext, GameRepository gameRepo)
        {
            _categoryService = categoryService;
            _dbContext = dbContext;
            _gameRepo = gameRepo;
        }
  
        [HttpGet("custom")]

        public async Task<IActionResult> GetGameCategory()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories(); // Assuming GetAllCategories is an asynchronous method
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetGameList")]

        public async Task<IActionResult> GetGameCategoryZ(int type)
        {
            try
            {

                var data = await _gameRepo.GetGameList(type);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("PIEChart")]
        public async Task<ActionResult<Dictionary<string, int>>> GetCategoryPivotTable(int type)
        {
            try
            {

                var data = await _gameRepo.GetPieList(type);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("BARChart")]
        public async Task<IActionResult> GetBarchat(int type)
        {
            try
            {
                var data = await _gameRepo.GetBarChat(type);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("LineChart")]
        public  IActionResult GetLineChart(int type)
        {
            // Series data

            object DownloadableSeries = null;
            object OnlineSeries = null;

            object seriesList = null;

            if (type == 1)
            {
                // Initialize series data inside the if block
                DownloadableSeries = new
                {
                    name = "Downloadable",
                    data = new[] { 241, 379, 297, 298, 324, 302, 381, 368, 337, 342, 310 }
                };

                 seriesList = new[] { DownloadableSeries};
         
            }
            else
            {
               
                OnlineSeries = new
                {
                    name = "Online",
                    data = new[] { 243, 117, 243, 201, 300, 160, 309, 321, 292, 197, 292 }
                };


                 seriesList = new[] { OnlineSeries };

            }

            // Combine series into a list
        

            return Ok(seriesList);
           
        }



    }
}

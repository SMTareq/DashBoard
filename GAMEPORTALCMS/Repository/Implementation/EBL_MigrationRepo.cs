using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class EBL_MigrationRepo
    {
        private readonly AppDBContext _dbContext;

        public EBL_MigrationRepo(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EBL_MigrationDTO>> GetGameList(int type)
        {
            List<EBL_MigrationDTO> gameInfos = new List<EBL_MigrationDTO>();

            gameInfos = await (from x in _dbContext.EBL_Migrations.AsNoTracking()

                               select new EBL_MigrationDTO
                               {
                                   DWDOCID = x.DWDOCID,
                                   DWNAME = x.DWNAME,
                                   DOCUMENT_NAME = x.DOCUMENT_NAME,
                                   STATUS = x.STATUS,
                               }).ToListAsync();


         

            //if (type == 2)
            //{
            //    gameInfos = await (from x in _dbContext.OnlineGamess.AsNoTracking()
            //                       join y in _dbContext.GameCategorys on x.CategoryId equals y.Id
            //                       select new GameInfoDTO
            //                       {
            //                           Id = x.Id,
            //                           GameName = x.GameName,
            //                           Name = x.GameName,
            //                           Description = x.Description,
            //                           PortraitImage = Config.BaseImageURL + x.PortraitURL,
            //                           BannerImage = Config.BaseImageURL + x.BannerURL,
            //                           PreviewImage = Config.BaseImageURL + x.PreviewURL,
            //                           GameURL = x.GameURL,
            //                           CategoryId = x.CategoryId,
            //                           CategoryName = y.CategoryName,
            //                           GameType = x.Type,
            //                           Status = x.IsActive,
            //                           BannerImageMockURL = x.BannerURL,
            //                           PortraitImageMockURL = x.PortraitURL,
            //                           PreviewImageMockURL = x.PreviewURL,
            //                       }).ToListAsync();
            //}


            return gameInfos;
        }

    }
}

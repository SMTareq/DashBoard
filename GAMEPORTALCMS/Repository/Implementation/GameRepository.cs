using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Models.Request;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Drawing;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class GameRepository
    {

        public readonly AppDBContext _context;
        public GameRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<GameInfoDTO>> GetGameList(int type)
        {
            List<GameInfoDTO> gameInfos = new List<GameInfoDTO>();
            if (type == 1)
            {
                gameInfos = await (from x in _context.DownloadableGames.AsNoTracking()
                                   join y in _context.GameCategorys on x.CategoryId equals y.Id
                                   select new GameInfoDTO
                                   {
                                       Id = x.Id,
                                       AndroidVersion = x.AndroidVersion,
                                       GameName = x.GameName,
                                       Name = x.GameName,
                                       Description = x.Description,
                                       PortraitImage = Config.BaseImageURL + x.PortraitURL,
                                       BannerImage = Config.BaseImageURL + x.BannerURL,
                                       PreviewImage = Config.BaseImageURL + x.PreviewURL,
                                       APKLocation = Config.PhysicalFileURL + x.PhysicalLocation,
                                       CategoryId = x.CategoryId,
                                       CategoryName = y.CategoryName,
                                       GamePrice = x.GamePrice,
                                       GameType = x.Type,
                                       Size = x.Size,
                                       Status = x.IsActive,
                                       BannerImageMockURL = x.BannerURL,
                                       PortraitImageMockURL = x.PortraitURL,
                                       PreviewImageMockURL = x.PreviewURL,
                                       APKMockURL = x.PhysicalLocation
                                   }).ToListAsync();
            }

            if (type == 2)
            {
                gameInfos = await (from x in _context.OnlineGamess.AsNoTracking()
                                   join y in _context.GameCategorys on x.CategoryId equals y.Id
                                   select new GameInfoDTO
                                   {
                                       Id = x.Id,
                                       GameName = x.GameName,
                                       Name = x.GameName,
                                       Description = x.Description,
                                       PortraitImage = Config.BaseImageURL + x.PortraitURL,
                                       BannerImage = Config.BaseImageURL + x.BannerURL,
                                       PreviewImage = Config.BaseImageURL + x.PreviewURL,
                                       GameURL = x.GameURL,
                                       CategoryId = x.CategoryId,
                                       CategoryName = y.CategoryName,
                                       GameType = x.Type,
                                       Status = x.IsActive,
                                       BannerImageMockURL = x.BannerURL,
                                       PortraitImageMockURL = x.PortraitURL,
                                       PreviewImageMockURL = x.PreviewURL,
                                   }).ToListAsync();
            }


            return gameInfos;
        }




        public async Task<List<PieChartDTO>> GetPieList(int type)
        {
            List<PieChartDTO> gameInfos = new List<PieChartDTO>();
            if (type == 1)
            {

                // Retrieve all DownloadableGames and GameCategories
                var downloadableGames = await _context.DownloadableGames.ToListAsync();
                var gameCategories = await _context.GameCategorys.ToListAsync();

                // Group DownloadableGames in-memory
                var groupedData = downloadableGames
                    .GroupBy(main => main.CategoryId)
                    .ToList();

                // Perform left join in-memory
                var result = (from categoryGroup in groupedData
                              join subCategory in gameCategories on categoryGroup.Key equals subCategory.Id into joined
                              from subCategory in joined.DefaultIfEmpty()
                              select new PieChartDTO
                              {
                                  CategoryName = subCategory != null ? subCategory.CategoryName : "No Category",
                                  Total = categoryGroup.Count()
                              })
                             .ToList();

                gameInfos = result.ToList();

                Dictionary<string, int> pivotTable = result
                   .GroupBy(x => x.CategoryName)
                   .ToDictionary(
                    group => group.Key,
                    group => group.Sum(item => item.Total)
                     );

            }

            if (type == 2)
            {

                // Retrieve all DownloadableGames and GameCategories
                var downloadableGames = await _context.OnlineGamess.ToListAsync();
                var gameCategories = await _context.GameCategorys.ToListAsync();

                // Group DownloadableGames in-memory
                var groupedData = downloadableGames
                    .GroupBy(main => main.CategoryId)
                    .ToList();

                // Perform left join in-memory
                var result = (from categoryGroup in groupedData
                              join subCategory in gameCategories on categoryGroup.Key equals subCategory.Id into joined
                              from subCategory in joined.DefaultIfEmpty()
                              select new PieChartDTO
                              {
                                  CategoryName = subCategory != null ? subCategory.CategoryName : "No Category",
                                  Total = categoryGroup.Count()
                              })
                             .ToList();


                gameInfos = result.ToList();


                Dictionary<string, int> pivotTable = result
                   .GroupBy(x => x.CategoryName)
                   .ToDictionary(
                    group => group.Key,
                    group => group.Sum(item => item.Total)
                     );
            }


            return gameInfos;
        }




        public Task<List<BarChart>> GetBarChat(int type)
        {

            //var query = await (from billboard in _context.Billboards
            //                   join gameType in _context.GameTypes on billboard.GameType equals gameType.Id into gameTypeGroup
            //                   from gameType in gameTypeGroup.DefaultIfEmpty()
            //                   group billboard by new
            //                   {
            //                       GameTypeName = gameType.TypeName,
            //                       Year = billboard.CreatedDate
            //                   } into groupedData
            //                   orderby groupedData.Key.GameTypeName, groupedData.Key.Year
            //                   select new BarChartDTO
            //                   {
            //                       TypeName = groupedData.Key.GameTypeName,
            //                       Year = groupedData.Key.Year,
            //                       Total = groupedData.Count()
            //                   }).ToListAsync();
            //var result = from billboard in _context.Billboards
            //             join gameType in _context.GameTypes on billboard.GameType equals gameType.Id into gameTypeGroup
            //             from gt in gameTypeGroup.DefaultIfEmpty()
            //             group new { billboard, gt } by new
            //             {
            //                 gt.Id,
            //                 gt.TypeName,
            //                 Year = billboard.CreatedDate
            //             } into grouped
            //             select new
            //             {
            //                 TypeName = grouped.Key.TypeName,
            //                 Year = grouped.Key.Year,
            //                 SerialConcatenated = string.Join(", ", grouped.Select(x => x.billboard.Serial))
            //             };

            List<BarChart> chartDataList;

            if (type == 1)
            {
                chartDataList = new List<BarChart>
                {
                 // new BarChart { Name = "Year 2023", Data = new List<int> { 310, 300,200,250,350 } },
                 // new BarChart { Name = "Year 2024", Data = new List<int> { 214, 250,350,355,210 } }
                };
            }
            else
            {
                chartDataList = new List<BarChart>
                {
                  // new BarChart { Name = "Year 2023", Data = new List<int> { 350, 330,222,253,350} },
                  // new BarChart { Name = "Year 2024", Data = new List<int> { 420, 280,220,336,444} }
                };
            }

            return Task.FromResult(chartDataList);

        }



        public async Task<List<GameCategory>> GetGameCategory()
        {
            var data = await _context.GameCategorys.AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<bool> UpdateGameDetails(GameInfoDTO game, string sessionUser)
        {
            bool result = true;
            if (game.GameType == (int)GameTypeEnum.Donwloadable)
            {
                var download = await _context.DownloadableGames.FirstOrDefaultAsync(x => x.Id == game.Id);
                if (download != null)
                {
                    download.AndroidVersion = game.AndroidVersion;
                    download.BannerURL = game.BannerImageMockURL;
                    download.CategoryId = game.CategoryId;
                    download.Description = game.Description;
                    download.GameName = game.GameName;
                    download.GamePrice = game.GamePrice;
                    download.IsActive = game.Status;
                    download.PhysicalLocation = game.APKMockURL;
                    download.PortraitURL = game.PortraitImageMockURL;
                    download.PreviewURL = game.PreviewImageMockURL;
                    download.Size = game.Size;
                    download.UpdatedBy = sessionUser;
                    download.UpdatedDate = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }

            if (game.GameType == (int)GameTypeEnum.Online)
            {
                var online = await _context.OnlineGamess.FirstOrDefaultAsync(x => x.Id == game.Id);
                if (online != null)
                {
                    online.CategoryId = game.CategoryId;
                    online.Description = game.Description;
                    online.GameName = game.GameName;
                    online.IsActive = game.Status;
                    online.GameURL = game.GameURL;
                    online.BannerURL = game.BannerImageMockURL;
                    online.PortraitURL = game.PortraitImageMockURL;
                    online.PreviewURL = game.PreviewImageMockURL;
                    online.UpdatedBy = sessionUser;
                    online.UpdatedDate = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }


            return result;
        }


        public async Task<bool> AddGameS(GameInfoDTO game, string sessionUser)
        {
            bool result = true;
            if (game.GameType == (int)GameTypeEnum.Donwloadable)
            {
                var download = new DownloadableGame();
                if (download != null)
                {
                    download.AndroidVersion = game.AndroidVersion;
                    download.BannerURL = game.BannerImageMockURL;
                    download.CategoryId = game.CategoryId;
                    download.Description = game.Description;
                    download.GameName = game.GameName;
                    download.GamePrice = game.GamePrice;
                    download.IsActive = game.Status;
                    download.PhysicalLocation = game.APKMockURL;
                    download.PortraitURL = game.PortraitImageMockURL;
                    download.PreviewURL = game.PreviewImageMockURL;
                    download.Size = game.Size;
                    download.CreatedBy = sessionUser;
                    download.CreatedDate = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }

            if (game.GameType == (int)GameTypeEnum.Online)
            {
                var online = new OnlineGames();
                if (online != null)
                {
                    online.CategoryId = game.CategoryId;
                    online.Description = game.Description;
                    online.GameName = game.GameName;
                    online.IsActive = game.Status;
                    online.GameURL = game.GameURL;
                    online.BannerURL = game.BannerImageMockURL;
                    online.PortraitURL = game.PortraitImageMockURL;
                    online.PreviewURL = game.PreviewImageMockURL;
                    online.CreatedBy = sessionUser;
                    online.CreatedDate = DateTime.Now;

                }
                await _context.SaveChangesAsync();
            }


            return result;
        }

        public async Task<List<BillboardDTO>> GetBillboardList(int portalValue)
        {
            var data = await (from bill in _context.Billboards.AsNoTracking().Where(x => (x.Client & portalValue) == portalValue)
                              join r in _context.DownloadableGames.AsNoTracking() on bill.GameId equals r.Id
                              into dg
                              from downloadGames in dg.DefaultIfEmpty()
                              join q in _context.OnlineGamess.AsNoTracking() on bill.GameId equals q.Id
                              into og
                              from onlineGame in og.DefaultIfEmpty()
                              select new BillboardDTO
                              {
                                  Id = bill.Id,
                                  GameId = bill.GameId,
                                  GameName = bill.GameType == (int)GameTypeEnum.Donwloadable ? downloadGames.GameName : onlineGame.GameName,
                                  GameTypeId = bill.GameType,
                                  ImageUrl = Config.BaseImageURL + bill.ImageURL,
                                  BannerImageMockURL = bill.ImageURL,
                                  IsActive = bill.IsActive,
                                  Portal = bill.Client,
                                  Serial = bill.Serial,
                                  ClientValueDetails = bill.ClientValueDetails

                              }).OrderByDescending(s => s.IsActive).ThenBy(x => x.Serial).ToListAsync();
            return data;
        }

        public async Task<List<GameInfoDTO>> GameSearchByTerm(int portalValue, int gameType, string searchTerm)
        {
            List<GameInfoDTO> gameInfos = new List<GameInfoDTO>();
            if (gameType == 1)
            {
                gameInfos = await (from x in _context.DownloadableGames.AsNoTracking()
                                   where x.GameName.Contains(searchTerm)
                                   select new GameInfoDTO
                                   {
                                       Id = x.Id,
                                       GameName = x.GameName,
                                   }).ToListAsync();
            }

            if (gameType == 2)
            {
                gameInfos = await (from x in _context.OnlineGamess.AsNoTracking()
                                   where x.GameName.Contains(searchTerm)
                                   select new GameInfoDTO
                                   {
                                       Id = x.Id,
                                       GameName = x.GameName,
                                   }).ToListAsync();
            }
            return gameInfos;
        }

        public async Task<bool> SaveBillboard(SV_Billboard billboard, string sessinUser)
        {

            try
            {
                if (billboard.Id > 0)
                {
                    var exist = await _context.Billboards.FirstOrDefaultAsync(x => x.Id == billboard.Id);
                    if (exist != null)
                    {
                        exist.GameId = billboard.GameId;
                        exist.Client = billboard.Portal;
                        exist.UpdatedBy = sessinUser;
                        exist.UpdatedDate = DateTime.Now;
                        exist.GameType = billboard.GameTypeId;
                        exist.ImageURL = billboard.BannerImageMockURL;
                        exist.IsActive = billboard.IsActive;
                        exist.Serial = billboard.Serial;
                        exist.ClientValueDetails = billboard.ClientDetails;
                        await _context.SaveChangesAsync();

                    }


                }
                else
                {
                    var bill = new Billboard
                    {
                        GameId = billboard.GameId,
                        Client = billboard.Portal,
                        CreatedBy = sessinUser,
                        CreatedDate = DateTime.Now,
                        GameType = billboard.GameTypeId,
                        ImageURL = billboard.BannerImageMockURL,
                        IsActive = billboard.IsActive,
                        Serial = billboard.Serial,
                        ClientValueDetails = billboard.ClientDetails,
                    };

                    await _context.Billboards.AddAsync(bill);
                    await _context.SaveChangesAsync();
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }

        public async Task<bool> SaveGame(List<GameUpload> uploads, string type)
        {
            try
            {
                foreach (GameUpload upload in uploads)
                {
                    if (type == "downloadable")
                    {
                        var exist = await _context.DownloadableGames.FirstOrDefaultAsync(x => x.GameName.ToLower() == upload.GameName.ToLower());
                        var Caterory = await _context.GameCategorys.FirstOrDefaultAsync(x => x.CategoryName.ToLower() == upload.Category.ToLower());
                        if (exist == null && Caterory != null)
                        {
                            var downloadable = new DownloadableGame
                            {
                                GameName = upload.GameName,
                                GameCode = "DWN-"+ upload.GameName,
                                PreviewURL = upload.PreviewURL,
                                PortraitURL = upload.PortraitURL,
                                BannerURL = upload.BannerURL,
                                CategoryId = Caterory.Id,
                                Description = upload.Description,
                                PhysicalLocation = upload.PhysicalLocation,
                                GamePrice = upload.GamePrice,
                                AndroidVersion = upload.AndroidVersion,
                                Size = upload.Size,
                                //OwnerCode = upload.OwnerCode,
                                Type = 1,
                                IsActive = true
                            };

                            await _context.DownloadableGames.AddAsync(downloadable);
                            await _context.SaveChangesAsync();
                        }
                    }

                    if (type == "online")
                    {
                        var exist = await _context.OnlineGamess.FirstOrDefaultAsync(x => x.GameName.ToLower() == upload.GameName.ToLower());
                        var Caterory = await _context.GameCategorys.FirstOrDefaultAsync(x => x.CategoryName.ToLower() == upload.Category.ToLower());
                        if (exist == null && Caterory != null)
                        {
                            var online = new OnlineGames
                            {
                                GameName = upload.GameName,
                                GameCode = "ONL"+upload.GameName,
                                PreviewURL = upload.PreviewURL,
                                PortraitURL = upload.PortraitURL,
                                BannerURL = upload.BannerURL,
                                CategoryId = Caterory.Id,
                                Description = upload.Description,
                                GameURL = upload.PhysicalLocation,
                                Type = 2,
                                IsActive = true
                            };

                            await _context.OnlineGamess.AddAsync(online);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<GameInfoDTO> GetSingleGame(int gameId, int type)
        {
            GameInfoDTO gameInfos = new GameInfoDTO();
            IQueryable<GameInfoDTO> query = null;
            if (type == 1)
            {
                query = (from x in _context.DownloadableGames.AsNoTracking().Where(x => x.Id == gameId)
                         join y in _context.GameCategorys on x.CategoryId equals y.Id
                         select new GameInfoDTO
                         {
                             Id = x.Id,
                             AndroidVersion = x.AndroidVersion,
                             GameName = x.GameName,
                             Name = x.GameName,
                             Description = x.Description,
                             PortraitImage = Config.BaseImageURL + x.PortraitURL,
                             BannerImage = Config.BaseImageURL + x.BannerURL,
                             PreviewImage = Config.BaseImageURL + x.PreviewURL,
                             APKLocation = Config.PhysicalFileURL + x.PhysicalLocation,
                             CategoryId = x.CategoryId,
                             CategoryName = y.CategoryName,
                             GamePrice = x.GamePrice,
                             GameType = x.Type,
                             Size = x.Size,
                             Status = x.IsActive,
                             BannerImageMockURL = x.BannerURL,
                             PortraitImageMockURL = x.PortraitURL,
                             PreviewImageMockURL = x.PreviewURL,
                             APKMockURL = x.PhysicalLocation
                         });
            }

            if (type == 2)
            {
                query = (from x in _context.OnlineGamess.AsNoTracking().Where(x => x.Id == gameId)
                         join y in _context.GameCategorys on x.CategoryId equals y.Id
                         select new GameInfoDTO
                         {
                             Id = x.Id,
                             GameName = x.GameName,
                             Name = x.GameName,
                             Description = x.Description,
                             PortraitImage = Config.BaseImageURL + x.PortraitURL,
                             BannerImage = Config.BaseImageURL + x.BannerURL,
                             PreviewImage = Config.BaseImageURL + x.PreviewURL,
                             GameURL = x.GameURL,
                             CategoryId = x.CategoryId,
                             CategoryName = y.CategoryName,
                             GameType = x.Type,
                             Status = x.IsActive,
                             BannerImageMockURL = x.BannerURL,
                             PortraitImageMockURL = x.PortraitURL,
                             PreviewImageMockURL = x.PreviewURL,
                         });
            }

            gameInfos = await query.FirstOrDefaultAsync();


            return gameInfos;
        }
    }
}

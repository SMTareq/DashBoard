using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class PatchGameRepository
    {
        private readonly AppDBContext _context;

        public PatchGameRepository(AppDBContext dbContext)
        {
            _context = dbContext;
        }


        public async Task<List<SimpleDTO>> GetPatchGameList(int patchId, int gameTypeId)
        {
            List<SimpleDTO> gameInfos = new List<SimpleDTO>();
            if (gameTypeId == 1)
            {
                gameInfos = await (from y in _context.PatchGames.AsNoTracking()
                                   .OrderBy(x => x.Serial)
                                   .Where(x => x.GamePatchId == patchId)
                                   join x in _context.DownloadableGames on y.GameId equals x.Id
                                   select new SimpleDTO
                                   {
                                       Id = x.Id,
                                       Name = x.GameName,
                                       PreviewImageURL = Config.BaseImageURL + x.PreviewURL,
                                   }).ToListAsync();
            }
            if (gameTypeId == 2)
            {
                gameInfos = await (from y in _context.PatchGames.AsNoTracking()
                                   .OrderBy(x => x.Serial)
                                   .Where(x => x.GamePatchId == patchId)
                                   join x in _context.OnlineGamess on y.GameId equals x.Id
                                   select new SimpleDTO
                                   {
                                       Id = x.Id,
                                       Name = x.GameName,
                                       PreviewImageURL = Config.BaseImageURL + x.PreviewURL,
                                   }).ToListAsync();
            }


            return gameInfos;
        }


        public async Task<List<SimpleDTO>> GetGameList(int clientValue, int type)
        {
            List<SimpleDTO> gameInfos = new List<SimpleDTO>();
            if (type == 1)
            {
                gameInfos = await (from x in _context.DownloadableGames.AsNoTracking()
                                   join y in _context.GameCategorys on x.CategoryId equals y.Id
                                   select new SimpleDTO
                                   {
                                       Id = x.Id,
                                       Name = x.GameName,
                                       PreviewImageURL = Config.BaseImageURL + x.PreviewURL,
                                   }).ToListAsync();
            }

            if (type == 2)
            {
                gameInfos = await (from x in _context.OnlineGamess.AsNoTracking()
                                   join y in _context.GameCategorys on x.CategoryId equals y.Id
                                   select new SimpleDTO
                                   {
                                       Id = x.Id,
                                       Name = x.GameName,
                                       PreviewImageURL = Config.BaseImageURL + x.PreviewURL,
                                   }).ToListAsync();
            }


            return gameInfos;
        }


        public async Task UpdatePatchGames(List<SimpleDTO> aList, int patchId, string userName)
        {
            try
            {
                int serial = 1;
                var patchGamesToUpdate = new List<PatchGame>();
                var patchGamesToInsert = new List<PatchGame>();

                foreach (var item in aList)
                {
                    var game = await _context.PatchGames.FirstOrDefaultAsync(x => x.GameId == item.Id && x.GamePatchId == patchId);

                    if (game != null)
                    {
                        game.Serial = serial;
                        game.UpdatedDate = DateTime.Now;
                        game.UpdatedBy = userName;
                        patchGamesToUpdate.Add(game);
                    }
                    else
                    {
                        patchGamesToInsert.Add(new PatchGame
                        {
                            GamePatchId = patchId,
                            GameId = item.Id,
                            Serial = serial,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now
                        });
                    }

                    serial++;
                }
                _context.PatchGames.UpdateRange(patchGamesToUpdate);
                await _context.PatchGames.AddRangeAsync(patchGamesToInsert);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }


        public async Task<bool> DeletePatchGame(int id, int patchId)
        {
            bool result = true;
            try
            {
                var game = await _context.PatchGames.FirstOrDefaultAsync(x => x.GameId == id && x.GamePatchId == patchId);
                if (game != null)
                {
                    _context.PatchGames.Remove(game);
                    await _context.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


    }
}

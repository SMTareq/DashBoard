using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class PatchRepository
    {
        private readonly AppDBContext _dbContext;

        public PatchRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GamePatch> GetPatchById(int id)
        {
            return await _dbContext.GamePatchs.FindAsync(id);
        }

        public async Task<List<GameType>> GetgameType()
        {
            return await _dbContext.GameTypes.ToListAsync();
        }


        public async Task<List<GamePatchDTO>> GetAllPatch()
        {
            var data = await _dbContext.GamePatchs
                         .Join(_dbContext.GameTypes,
                             p => p.Type,
                             q => q.Id,
                             (p, q) => new GamePatchDTO
                             {
                                 Id = p.Id,
                                 PatchName = p.PatchName,
                                 IsActive = p.IsActive,
                                 PatchType = p.Type,
                                 PatchTypeName = q.TypeName,
                                 Serial = p.Serial
                             })
                         .ToListAsync();
            return data;
        }

        public async Task<List<GamePatchDTO>> GetPatchByPortal(int portalValue)
        {
            var data = await _dbContext.GamePatchs.Where(x => (x.Client & portalValue) == portalValue)
                         .Join(_dbContext.GameTypes,
                             p => p.Type,
                             q => q.Id,
                             (p, q) => new GamePatchDTO
                             {
                                 Id = p.Id,
                                 PatchName = p.PatchName,
                                 IsActive = p.IsActive,
                                 PatchType = p.Type,
                                 PatchTypeName = q.TypeName,
                                 Serial = p.Serial
                             })
                         .ToListAsync();
            return data;
        }

        public async Task<bool> SavewPatch(GamePatchDTO cat, string sessinUser)
        {
            bool result = true;
            try
            {
                var data = await _dbContext.GamePatchs.FirstOrDefaultAsync(x => x.Id == cat.Id);
                if (data != null)
                {
                    data.Serial = cat.Serial;
                    data.PatchName = cat.PatchName;
                    data.IsActive = cat.IsActive;
                    data.UpdatedBy = sessinUser;
                    data.UpdatedDate = DateTime.Now;
                    data.Client = cat.PortalValue;
                    data.Type = cat.PatchType;
                    await _dbContext.SaveChangesAsync();

                }
                else
                {
                    var gmp = new GamePatch
                    {
                        PatchName = cat.PatchName,
                        IsActive = cat.IsActive,
                        Serial = cat.Serial,
                        Type = cat.PatchType,
                        CreatedBy = sessinUser,
                        CreatedDate = DateTime.Now,
                        Client = cat.PortalValue


                    };
                    _dbContext.GamePatchs.Add(gmp);
                    await _dbContext.SaveChangesAsync();

                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }



    }
}

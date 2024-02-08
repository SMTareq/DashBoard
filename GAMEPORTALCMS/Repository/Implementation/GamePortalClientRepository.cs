using GAMEPORTALCMS.Controllers;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class GamePortalClientRepository
    {
        private readonly AppDBContext _dbContext;
        public GamePortalClientRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GamePortalClient>> GetAllPortalClint()
        {
            var data = await _dbContext.GamePortalClients.Select(x => new GamePortalClient
            {
                Id = x.Id,
                ClientName = x.ClientName,
                ClientValue = x.ClientValue,
            }).OrderBy(x => x.ClientName).ToListAsync();

            return data;
        }

        public async Task<List<GamePortalClient>> GetActivePortal()
        {
            var data = await _dbContext.GamePortalClients.Where(x => x.IsActive == true)
                .Select(x => new GamePortalClient
            {
                Id = x.Id,
                ClientName = x.ClientName,
                ClientValue = x.ClientValue,
            }).OrderBy(x => x.ClientName).ToListAsync();

            return data;
        }


        public async Task<bool> SaveGamePortalClient(GamePortalClient Portal, string sessinUser)
        {
            try
            {
                if (Portal.Id > 0)
                {
                    var PortalClint = await _dbContext.GamePortalClients.FirstOrDefaultAsync(x => x.Id == Portal.Id);
                    if (PortalClint != null)
                    {
                        PortalClint.ClientName = Portal.ClientName;

                        await _dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    var PortalClint = new GamePortalClient
                    {
                        ClientName = Portal.ClientName,
                        ClientValue = Convert.ToInt32(Math.Pow(2, _dbContext.GamePortalClients.Count())),
                        IsActive = true
                    };
                    _dbContext.GamePortalClients.Add(PortalClint);
                    await _dbContext.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}

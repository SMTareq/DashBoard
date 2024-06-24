using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class CommonDataLoadRepository
    {
        public readonly AppDBContext _context;

        public CommonDataLoadRepository(AppDBContext context)
        {
            this._context = context;
        }

        //public async Task<List<GameType>> GetGameType()
        //{
        //    var data = await _context.GameTypes.AsNoTracking().ToListAsync();
        //    return data;

        //}
    }
}

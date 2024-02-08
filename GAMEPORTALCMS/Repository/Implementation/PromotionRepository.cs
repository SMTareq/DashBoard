using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class PromotionRepository
    {

        public readonly AppDBContext _context;
        public PromotionRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<PromotionDTO>> GetPromoList(int portalValue)
        {
            var data = await (from bill in _context.Promotions.AsNoTracking().Where(x => (x.Client & portalValue) == portalValue)
                              select new PromotionDTO
                              {
                                  Id = bill.Id,
                                  Description = bill.Description,
                                  EventUrl = bill.EventUrl,
                                  PromotionName = bill.PromotionName,
                                  Image = Config.BaseImageURL + bill.Image,
                                  ImageMockURL = bill.Image,
                                  IsActive = bill.IsActive,
                                  PortalValue = bill.Client,
                                  Serial = bill.Serial,
                                  ClientValueDetails = bill.ClientValueDetails
                              }).OrderByDescending(s => s.IsActive).ThenBy(x => x.Serial).ToListAsync();
            return data;
        }


        public async Task<bool> SavePromotion(PromotionDTO item, string sessinUser)
        {

            try
            {
                if (item.Id > 0)
                {
                    var exist = await _context.Promotions.FirstOrDefaultAsync(x => x.Id == item.Id);
                    if (exist != null)
                    {
                        exist.PromotionName = item.PromotionName;
                        exist.EventUrl = item.EventUrl;
                        exist.Description = item.Description;
                        exist.Client = item.PortalValue;
                        exist.ClientValueDetails = item.ClientValueDetails;
                        exist.CreatedBy = sessinUser;
                        exist.UpdatedDate = DateTime.Now;
                        exist.Image = item.ImageMockURL;
                        exist.IsActive = item.IsActive;
                        exist.Serial = item.Serial;
                        await _context.SaveChangesAsync();

                    }


                }
                else
                {
                    var promo = new Promotion
                    {
                        PromotionName = item.PromotionName,
                        EventUrl = item.EventUrl,
                        Description = item.Description,
                        Client=item.PortalValue,
                        ClientValueDetails= item.ClientValueDetails,
                        CreatedBy = sessinUser,
                        CreatedDate = DateTime.Now,
                        Image= item.ImageMockURL,
                        IsActive= item.IsActive,
                        Serial = item.Serial,
                        
                    };

                    await _context.Promotions.AddAsync(promo);
                    await _context.SaveChangesAsync();
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

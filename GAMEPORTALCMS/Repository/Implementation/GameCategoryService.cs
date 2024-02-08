using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Models.Request;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class GameCategoryService
    {
        private readonly AppDBContext _dbContext;

        public GameCategoryService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameCategory> GetCategoryById(int id)
        {
            return await _dbContext.GameCategorys.FindAsync(id);
        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var data = await _dbContext.GameCategorys.Select(x => new CategoryDTO
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                IconUrl = x.IconURL,
                ImageUrl = x.Image,
                Mock_IconUrl = Config.BaseImageURL + x.IconURL,
                Mock_ImageUrl = Config.BaseImageURL + x.Image,
                Seial = x.Serial,
                IsActive = x.IsActive
            }).OrderBy(x => x.Seial).ToListAsync();

            return data;
        }
        public async Task<bool> SaveCategory(CategoryDTO cat, string sessinUser)
        {
            try
            {
                if (cat.Id > 0)
                {
                    var category = await _dbContext.GameCategorys.FirstOrDefaultAsync(x => x.Id == cat.Id);
                    if (category != null)
                    {
                        category.CategoryName = cat.CategoryName;
                        category.IconURL = cat.IconUrl;
                        category.Image = cat.ImageUrl;
                        category.Serial = cat.Seial;
                        category.IsActive = cat.IsActive;
                        category.UpdatedBy = sessinUser;
                        category.UpdatedDate = DateTime.Now;
                        await _dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    var category = new GameCategory
                    {
                        CategoryName = cat.CategoryName,
                        IconURL = cat.IconUrl,
                        Image = cat.ImageUrl,
                        Serial = cat.Seial,
                        IsActive = cat.IsActive,
                        CreatedBy = sessinUser,
                        CreatedDate = DateTime.Now,
                    };

                    _dbContext.GameCategorys.Add(category);
                    await _dbContext.SaveChangesAsync();

                }

                return true;

            }
            catch(Exception ex)
            {

                return false;
            }
           
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _dbContext.GameCategorys.FindAsync(id);
            if (category != null)
            {
                _dbContext.GameCategorys.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}

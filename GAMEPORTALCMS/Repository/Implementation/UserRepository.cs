using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class UserRepository
    {
        private readonly AppDBContext _dbContext;

        public UserRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CMSUser> ValidateUser(string userName,string password)
        {
            CMSUser data = new CMSUser();
            try
            {
                 data = await _dbContext.CMSUsers.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);

            }
            catch (Exception ex)
            {
                string str = "dsa";

            }
            return data;
        }

        #region CMSUSERCRUD
        public async Task<List<CMSuserDTO>> GetAllCMSUser()
        {
            var data = await _dbContext.CMSUsers.Select(x => new CMSuserDTO
            {
                Id = x.Id,

                Username = x.Username,
                UserCode = x.UserCode,
                Password = x.Password,
                IsBlock = x.IsBlock,

            }).OrderBy(x => x.Username).ToListAsync();

            return data;
        }


        public async Task<bool> SaveCMSUser(CMSuserDTO cat, string sessinUser)
        {
            try
            {
                if (cat.Id > 0)
                {
                    var category = await _dbContext.CMSUsers.FirstOrDefaultAsync(x => x.Id == cat.Id);
                    if (category != null)
                    {
                        category.Username = cat.Username;
                        category.Password = cat.Password;
                        category.IsBlock = cat.IsBlock;
                        await _dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    var maxId = await _dbContext.CMSUsers.OrderByDescending(u => u.Id).FirstOrDefaultAsync();

                    var category = new CMSUser
                    {
                        Username = cat.Username,
                        UserCode = "CU"+ cat.Username + maxId.Id,
                        Password = cat.Password,
                        IsBlock = cat.IsBlock,
                        //CreatedBy = sessinUser,
                        //CreatedDate = DateTime.Now,
                    };
                    _dbContext.CMSUsers.Add(category);
                    await _dbContext.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }

        }
        #endregion

        #region UserCRUD
        public async Task<List<User>> GetAllUser()
        {
            var data = await _dbContext.Users.Select(x => new User
            {
                Id = x.Id,

                Name = x.Name,
                UserCode = x.UserCode,
                Password = x.Password,
                IsBlock = x.IsBlock,
                MSISDN = x.MSISDN,
                Email = x.Email,
                Client=x.Client,

            }).OrderBy(x => x.Name).ToListAsync();

            return data;
        }

        public async Task<bool> SaveUser(User cat, string sessinUser)
        {
            try
            {
                if (cat.Id > 0)
                {
                    var category = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == cat.Id);
                    if (category != null)
                    {
                        category.Name = cat.Name;
                        category.Email = cat.Email;
                        category.IsBlock = cat.IsBlock;
                        category.Client=cat.Client;
                        await _dbContext.SaveChangesAsync();
                    }
                }
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }

        }
        #endregion


    }
}

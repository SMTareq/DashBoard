using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using iRely.Common;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class UserRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly LoginDBContext _loginDBContext;

        public UserRepository(AppDBContext dbContext, LoginDBContext loginDBContext)
        {
            _dbContext = dbContext;
            _loginDBContext = loginDBContext;
        }

        public async Task<DWUser> ValidateUser(string userName,string password)
        {    
            DWUser data = new DWUser();
            try
            {
                 data = await _loginDBContext.DWUsers.FirstOrDefaultAsync(x => x.name == userName &&  x.email == password);
                // string Mew= Decrypt(data.password, "0123456789abcdef0123456789abcdef", "0123456789abcdef");
            }
            catch (Exception ex)
            {
                string str = "dsa";
            }
            return data;
        }

        public static string Encrypt(string plainText, string key, string iv)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string key, string iv)
        {
            byte[] encryptedBytes = Convert.FromBase64String(cipherText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
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

        public async Task<List<DWUser>> Get_EBL_UserMail_All()
        {
            var data = await _loginDBContext.DWUsers.Select(x => new DWUser
            {
                name = x.name,
                email = x.email,
            
            }).OrderBy(x => x.name).ToListAsync();

            return data;
        }



    }
}

using GAMEPORTALCMS.Data;
using System.Net.Mail;
using System.Net;
using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Models.Entity;
using AutoMapper;
using iRely.Common;

namespace GAMEPORTALCMS.Repository.Implementation
{
    // Mail Config is Inherit here. 
    public class MailGenerator : MailConfig
    {
        private readonly LoginDBContext _dbContext;
   
        public MailGenerator(LoginDBContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        #region Mail
        public Task<bool> SendMail(string User)
        {
                            
            try
            {
                using (MailMessage msg = new MailMessage())
                {
                    msg.From = new MailAddress(SystemMail);
                   // msg.To.Add("jawad@digiqoresystems.com");
                    msg.To.Add("tareq.creatrixbd@gmail.com");
                    msg.Subject = "Test Mail";
                    msg.Body = "Hi to you ... :)";

                    using (SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587))
                    {
                        client.EnableSsl = true;
                       // client.Credentials = new NetworkCredential("tareq.creatrixbd@gmail.com", "fjzcfiymtkjrbaej");
                        client.Credentials = new NetworkCredential(SystemMail, SystemMailPassword);
                        client.Timeout = 20000;

                        client.Send(msg);
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine("Error: " + ex.Message);
                return Task.FromResult(false);
            }
        }
        #endregion

    }
}

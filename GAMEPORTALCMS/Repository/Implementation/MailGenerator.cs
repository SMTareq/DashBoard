using GAMEPORTALCMS.Data;
using System.Net.Mail;
using System.Net;
using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Models.Entity;

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
            // Create the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(SystemMail);
            // mail.To.Add(User);
            mail.To.Add("smtareqalfaruk@gmail.com");
            mail.Subject = "Test Email From EBl Dashboard";
            mail.Body = "This is a test email body.";
            // Set up SMTP client
           // SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", portNumber);
            

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(SystemMail, SystemMailPassword);
            smtp.EnableSsl = true; // Set to true if your SMTP server requires SSL

          //  smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            try
            {
                smtp.Send(mail);
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                return Task.FromResult(false);
            }         
        }
        #endregion

    }
}

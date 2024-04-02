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
            //  // Create the email message
            //  MailMessage mail = new MailMessage();
            //  mail.From = new MailAddress(SystemMail);
            //  // mail.To.Add(User);
            //  mail.To.Add("smtareqalfaruk@gmail.com");
            //  mail.Subject = "Test Email From EBl Dashboard";
            //  mail.Body = "This is a test email body.";
            //  // Set up SMTP client
            // // SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);

            //  SmtpClient smtp = new SmtpClient("smtp.gmail.com", portNumber);

            //  smtp.UseDefaultCredentials = false;
            //  smtp.Credentials = new NetworkCredential(SystemMail, SystemMailPassword);
            //  smtp.EnableSsl = true; // Set to true if your SMTP server requires SSL

            ////  smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //  try
            //  {
            //      smtp.Send(mail);
            //      return Task.FromResult(true);
            //  }
            //  catch(Exception ex)
            //  {
            //      return Task.FromResult(false);
            //  }         

            //MailMessage msg = new MailMessage();

            //msg.From = new MailAddress("tareq.creatrixbd@gmail.com");
            //msg.To.Add("smtareqalfaruk@gmail.com");
            //msg.Subject = "Hello world! ";
            //msg.Body = "hi to you ... :)";
            //SmtpClient client = new SmtpClient();
            //client.UseDefaultCredentials = true;
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.EnableSsl = true;    
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("tareq.creatrixbd@gmail.com", "ycnmugxeeuapfff");          
            //client.Timeout = 20000;
            //try
            //{
            //    client.Send(msg);
            //    return Task.FromResult(true);
            //}
            //catch (Exception ex)
            //{
            //    return Task.FromResult(true);
            //}
            //finally
            //{
            //    msg.Dispose();
            //}

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("tareq.creatrixbd@gmail.com");
            msg.To.Add("smtareqalfaruk@gmail.com");
            msg.Subject = "Hello world!";
            msg.Body = "hi to you ... :)";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tareq.creatrixbd@gmail.com", "ycnmugxeeuapfff");
            client.Timeout = 20000;

            try
            {
                client.Send(msg);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(true);
            }
            finally
            {
                msg.Dispose();
            }



            //var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();
        }
        #endregion

    }
}

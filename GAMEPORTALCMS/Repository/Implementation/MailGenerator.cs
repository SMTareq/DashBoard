using GAMEPORTALCMS.Data;
using System.Net.Mail;
using System.Net;
using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Models.Entity;
using AutoMapper;
using iRely.Common;
using System.Text;

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

                    if (User == "admin@petersengineering.com")
                    {
                        msg.To.Add("dereklee@digiqoresystems.com");
                       // msg.To.Add("tareq.creatrixbd@gmail.com");                    
                    }
                    else
                    {
                        msg.To.Add(User);
                    }
                    msg.Subject = "Test Mail";
                    msg.Body = "Hi to you ... :)";

                    using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(SystemMail, SystemMailPassword);
                        //client.Credentials = new NetworkCredential(SystemMail, SystemMailPassword);
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

        private string GenerateHTMLTableFromData(List<object> data)
        {
            StringBuilder html = new StringBuilder();

            // Start the table
            html.Append("<table border='1'>");

            // Table header
            html.Append("<tr>");
            foreach (var property in data[0].GetType().GetProperties())
            {
                html.Append("<th>").Append(property.Name).Append("</th>");
            }
            html.Append("</tr>");

            // Table data
            foreach (var item in data)
            {
                html.Append("<tr>");
                foreach (var property in item.GetType().GetProperties())
                {
                    html.Append("<td>").Append(property.GetValue(item)).Append("</td>");
                }
                html.Append("</tr>");
            }

            // End the table
            html.Append("</table>");

            return html.ToString();
        }


        #endregion

    }
}

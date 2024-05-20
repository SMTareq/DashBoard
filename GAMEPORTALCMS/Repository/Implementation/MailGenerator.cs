using GAMEPORTALCMS.Data;
using System.Net.Mail;
using System.Net;
using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Models.Entity;
using AutoMapper;
using iRely.Common;
using System.Text;
using GAMEPORTALCMS.Models.DTO;
using OfficeOpenXml;

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
        public Task<bool> SendMail(MailSendDTO User)
        {                           
            try
            {
                using (MailMessage msg = new MailMessage())
                {
                    msg.From = new MailAddress(SystemMail);
                    // msg.To.Add("jawad@digiqoresystems.com");

                    if (User.MyProperty == "admin@petersengineering.com")
                    {
                        msg.To.Add("dereklee@digiqoresystems.com");
                        // msg.To.Add("tareq.creatrixbd@gmail.com");                    
                    }
                    else
                    {
                        msg.To.Add(User.MyProperty);
                    }

                    // Generate Excel file
                    string filePath = GenerateExcelFile(User);

                    msg.Subject = "EBL Test Mail";
                
                    //StringBuilder htmlBuilder = new StringBuilder();

                    //htmlBuilder.Append("<body>");

                    //// Start the HTML table
                    //htmlBuilder.Append("<table>");

                    //// Add table headers
                    //htmlBuilder.Append("<tr>");
                    //htmlBuilder.Append("<th>Data Class</th>");
                    //htmlBuilder.Append("<th>Account No</th>");
                    //htmlBuilder.Append("<th>Status</th>");
                    //htmlBuilder.Append("</tr>");

                    //// Iterate over each item in the list and add table rows
                    //foreach (MailBody person in User.mailBodies)
                    //{
                    //    htmlBuilder.Append("<tr>");
                    //    htmlBuilder.Append($"<td>{person.M_DATA_CLASS}</td>");
                    //    htmlBuilder.Append($"<td>{person.M_ACCOUNT_NO}</td>");
                    //    htmlBuilder.Append($"<td>{person.STATUS}</td>");
                    //    htmlBuilder.Append("</tr>");
                    //}
                    //// End the HTML table
                    //htmlBuilder.Append("</table>");

                    //htmlBuilder.Append("</body>");


                    // Convert StringBuilder to string
                    //string htmlTable = htmlBuilder.ToString();

                    msg.Body = "Please find the attached Excel file.";

                    // Add attachment
                    Attachment attachment = new Attachment(filePath);
                    msg.Attachments.Add(attachment);

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


        static string GenerateExcelFile(MailSendDTO personList)
        {
            string fileName = "EBL_PLC.xlsx";

            // Your code to generate the Excel file, using EPPlus or other libraries

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("EBL_PLC");

                // Set headers
                worksheet.Cells[1, 1].Value = "Data Class";
                worksheet.Cells[1, 2].Value = "Account No";
                worksheet.Cells[1, 3].Value = "Status";
                worksheet.Cells[1, 4].Value = "DW STORE DATETIME";

                worksheet.Cells[1, 5].Value = "Document Name";
                worksheet.Cells[1, 6].Value = "Branch Code";
                worksheet.Cells[1, 7].Value = "Product Type";
                worksheet.Cells[1, 8].Value = "Cif";
       
                // Populate data
                int row = 2;
                foreach (var person in personList.mailBodies)
                {
                    worksheet.Cells[row, 1].Value = person.M_DATA_CLASS;
                    worksheet.Cells[row, 2].Value = person.M_ACCOUNT_NO;
                    worksheet.Cells[row, 3].Value = person.STATUS;
                    worksheet.Cells[row, 4].Value = person.DWSTOREDATETIME;

                    worksheet.Cells[row, 5].Value = person.DocumentName;
                    worksheet.Cells[row, 6].Value = person.BranchCode;
                    worksheet.Cells[row, 7].Value = person.ProductType;
                    worksheet.Cells[row, 8].Value = person.Cif;

                    row++;
                }
                // Save the Excel package to a file
                var fileInfo = new FileInfo(fileName);
                package.SaveAs(fileInfo);

                return fileName;
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

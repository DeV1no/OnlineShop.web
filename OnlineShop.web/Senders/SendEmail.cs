using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace OnlineShop.Web.SendEmail
{
public class SendEmail
{
    public static void Send(string To, string Subject, string Body)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("danial.shokouhmanesh@gmail.com", "آنلاین شاپ");
        mail.To.Add(To);
        mail.Subject = Subject;
        mail.Body = Body;
        mail.IsBodyHtml = true;

        //System.Net.Mail.Attachment attachment;
        // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        // mail.Attachments.Add(attachment);

        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("danial.shokouhmanesh@gmail.com", "Dani09376963809");
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);
    }
}
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Library.App.Helper
{
    public class Help
    {
        public static List<object> FilterList()
        {
            List<object> filter = new List<object>();
            filter.Add(new { Value = 0, Text = "All" });
            filter.Add(new { Value = 1, Text = "Available Books" });
            filter.Add(new { Value = 2, Text = "My Books" });

            return filter;
        }
        public static void SendMail(string ToMail, string ToName, List<string> Parameters)
        {
            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            string Subject = "Library Reminder";
            string Body = "You took the following books in our library<br/><br/>{0}";

            string books = string.Join("<br/><br/>", Parameters);

            Body = string.Format(Body, books);
            SmtpClient client = new SmtpClient();
            //client.Port = 587;
            client.Port = 25;
            client.Host = (string)settingsReader.GetValue("connectSSL", typeof(String));
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            var fromEmailAddress = new MailAddress((string)settingsReader.GetValue("mailLoginName", typeof(String)), (string)settingsReader.GetValue("fromDisplayName", typeof(String)));
            var password = (string)settingsReader.GetValue("mailLoginPassword", typeof(String));
            client.Credentials = new System.Net.NetworkCredential(fromEmailAddress.Address, password);

            var toMailAddress = new MailAddress(ToMail, ToName);
            MailMessage mm = new MailMessage(fromEmailAddress, toMailAddress);
            mm.Body = Body;
            mm.Subject = Subject;
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.IsBodyHtml = true;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
            client.Dispose();
        }
    }
}
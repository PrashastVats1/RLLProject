using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Net;
using log4net;
using JobsPortal.Controllers;

namespace JobsPortal.Helper
{
    public static class MailHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MailHelper));
        public static bool SendJobApplicationEmail(string toEmail, string employeeName, string resumePath, string details)
        {
            bool IsBodyHtml = true;
            string subject = $"Application from {employeeName}";

            string body = $@"
                <h2>Job Application</h2>
                <p>The Candidate <strong>{employeeName}</strong> wishes to apply for your job.</p>
                <p>These are his details:</p>
                {details}
            ";

            bool emailStatus = SendEmailWithAttachment(toEmail, subject, body, IsBodyHtml, resumePath);
            return emailStatus;
        }

        private static bool SendEmailWithAttachment(string toEmail, string subject, string body, bool IsBodyHtml, string attachmentPath)
        {
            bool status = false;
            try
            {
                string HostAddress = ConfigurationManager.AppSettings["Host"].ToString();
                string FormEmailId = ConfigurationManager.AppSettings["MailFrom"].ToString();
                string Password = ConfigurationManager.AppSettings["Password"].ToString();
                string Port = ConfigurationManager.AppSettings["Port"].ToString();

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FormEmailId);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = IsBodyHtml;
                mailMessage.To.Add(new MailAddress(toEmail));

                if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                {
                    mailMessage.Attachments.Add(new Attachment(attachmentPath));
                }

                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = mailMessage.From.Address;
                networkCredential.Password = Password;

                smtp.Credentials = networkCredential;
                smtp.Port = Convert.ToInt32(Port);
                smtp.EnableSsl = true;

                smtp.Send(mailMessage);
                status = true;
            }
            catch (Exception e)
            {
                log.Error($"Failed to send email to {toEmail}. Exception: {e}");
                return status;
            }
            return status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EmailManager
    {
        public static bool SendEmail(string EmailTo, string EmailText, string Subject)
        {

            try
            {
                // Send Email 
                string SMTPAddress = "192.168.110.12";
                string EmailFrom = "recruitment@idlc.com";
                MailAddress from = new MailAddress(EmailFrom, "IDLC TALENT ACQUISITION SYSTEM");
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                mail.From = from;
                if (!String.IsNullOrEmpty(EmailTo))
                {
                    string[] arrEmailTo = EmailTo.Contains(";") ? EmailTo.Split(';') : new string[] { EmailTo };
                    foreach (string toEmail in arrEmailTo)
                    {

                        if (!String.IsNullOrEmpty(toEmail))
                        {
                            MailAddress toEmal = new MailAddress(toEmail);
                            mail.To.Add(toEmal);
                        }
                    }
                }

                mail.Subject = Subject; //"IDLC TALENT ACQUISITION SYSTEM OTP Code";


                mail.Body = EmailText;
                mail.IsBodyHtml = true;
                SmtpClient obj = new SmtpClient();
                obj.Host = SMTPAddress;
                obj.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

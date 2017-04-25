using System.Net.Mail;

namespace ZSZ.Common
{
    public class MailHelper
    {
        public static void SendMailBy163(string subject, string body, params string[] toAddress)
        {
            if (toAddress.Length == 0)
            {
                return;
            }

            string smtpServer = "smtp.163.com";
            string user = "rupengtest01@163.com";
            string pwd = "123rupeng";
            using (MailMessage mailMessage = new MailMessage())
            using (SmtpClient smtpClient = new SmtpClient(smtpServer))
            {
                foreach (var item in toAddress)
                {
                    mailMessage.To.Add(item);
                }
                
                mailMessage.Body = body;
                mailMessage.From = new MailAddress(user);
                mailMessage.Subject = subject;
                smtpClient.Credentials = new System.Net.NetworkCredential(user,pwd);//如果启用了“客户端授权码”，要用授权码代替密码
                smtpClient.Send(mailMessage);
            }
        }
    }
}

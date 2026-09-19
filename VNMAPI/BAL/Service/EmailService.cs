using BAL.Interface;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace BAL.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }     

        #region Send Email Code Function  
        /// <summary>  
        /// Send Email with cc bcc with given subject and message.  
        /// </summary>  
        /// <param name="ToEmail"></param>  
        /// <param name="cc"></param>  
        /// <param name="bcc"></param>  
        /// <param name="Subj"></param>  
        /// <param name="Message"></param>  
        public void SendEmail(String ToEmail, string cc, string bcc, String Subj, string Message)
        {
            try
            {

                //Reading sender Email credential from web.config file  

                string HostAdd = _config["EmailSetting:Host"].ToString();
                string FromEmailid = _config["EmailSetting:FromMail"].ToString();
                string Pass = _config["EmailSetting:Password"].ToString();
                string Port = _config["EmailSetting:Port"].ToString();
                bool IsEnableSSL = int.Parse(_config["EmailSetting:IsEnableSSL"].ToString()) == 1 ? true : false;
                bool IsUseDefaultCredentials = int.Parse(_config["EmailSetting:IsUseDefaultCredentials"].ToString()) == 1 ? true : false;


                //creating the object of MailMessage  
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
                mailMessage.Subject = Subj; //Subject of Email  
                mailMessage.Body = Message; //body or message of Email  
                mailMessage.IsBodyHtml = true;

                if (!string.IsNullOrEmpty(ToEmail))
                {
                    string[] ToMuliId = ToEmail.Split(',');
                    foreach (string ToEMailId in ToMuliId)
                    {
                        mailMessage.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id  
                    }
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] CCId = cc.Split(',');
                    foreach (string CCEmail in CCId)
                    {
                        mailMessage.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
                    }
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    string[] bccid = bcc.Split(',');
                    foreach (string bccEmailId in bccid)
                    {
                        mailMessage.Bcc.Add(new MailAddress(bccEmailId)); //Adding Multiple BCC email Id  
                    }
                }

                SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  
                smtp.Host = HostAdd;              //host of emailaddress for example smtp.gmail.com etc  

                //network and security related credentials  

                smtp.EnableSsl = IsEnableSSL;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = IsUseDefaultCredentials;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(Port);
                smtp.Send(mailMessage); //sending Email  
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}

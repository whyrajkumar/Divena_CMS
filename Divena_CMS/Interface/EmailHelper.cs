using Divena_CMS.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Divena_CMS.Interface
{
    public class EmailHelper : IEmailHelper
    {
        public readonly EmailSetting _emailSetting;
        public IHostingEnvironment _appEnvironment;
        public IConfiguration _configuration;
        public EmailHelper(IOptions<EmailSetting> options, IHostingEnvironment hostingEnvironment,IConfiguration configuration)
        {
            this._emailSetting = options.Value;
            _appEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public void EmailSender(EmailModel emailModel)
        {
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = _emailSetting.MailServer;
                    client.Port = _emailSetting.MailPort;
                    var credential = new NetworkCredential
                    {
                        UserName = _emailSetting.Sender,
                        Password = _emailSetting.Password
                    };
                    client.EnableSsl = false;


                    StringBuilder sb = new StringBuilder();
                    sb.Append("\r\n");
                    sb.Append(emailModel.Name);
                    sb.Append("\r\n");
                    sb.Append(emailModel.Subject);
                    sb.Append("\r\n");
                    sb.Append(emailModel.Message);


                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(_emailSetting.Sender, _emailSetting.SenderName);
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.To.Add(_emailSetting.Receiver);
                    mailMessage.Body = sb.ToString();
                    client.Send(mailMessage);
                }
            }
            catch
            {
                throw;
            }
        }


        public void SendMail(string email, string subject, string message)
        {
            try
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                var msz = new MailMessage();
                msz.From = new MailAddress("sender@gmail.com");//Email which you are getting 
                                                                //from contact us page 
                msz.To.Add("receiver@gmail.com");//Where mail will be sent 
                msz.Subject = subject;
                msz.Body = message;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = "mail.gmail.com";
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential
                ("sender@gmail.com", "11naCg8*");
                smtp.EnableSsl = false;
                smtp.Send(msz);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SendCustomEmailAsync(string visitName, string email, string subject, string message)
        {
            try
            {

                var msz = new MailMessage();
                msz.From = new MailAddress(_emailSetting.Sender, _emailSetting.SenderName);//Email which you are getting 
                                                                                           //from contact us page 
                msz.To.Add(_emailSetting.Receiver);//Where mail will be sent 
                msz.Subject = subject;
                msz.Body = FormattedBody(visitName, email, subject, message);
                msz.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = _emailSetting.MailServer;
                smtp.Port = _emailSetting.MailPort;
                smtp.Credentials = new System.Net.NetworkCredential(_emailSetting.Sender, _emailSetting.Password);
                //smtp.EnableSsl = _emailSetting.EnableSsl;
                smtp.EnableSsl= Convert.ToBoolean(_configuration.GetSection("EmailSetting").GetSection("EnableSsl").Value);
                smtp.Send(msz);


                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private string FormattedBody(object name, object email, object subject, object message)
        {
            var senderInfo = String.Format(
          "<b>From</b>: {0}<br/><b>Email</b>: {1}<br/><b>Subject</b>: {2}<br/><br/>",
          name, email, subject);
            return senderInfo + message;
        }

        public IEnumerable<string> GetClientIP(string localIP)
        {
            string ip = localIP;

            //https://en.wikipedia.org/wiki/Localhost
            //127.0.0.1    localhost
            //::1          localhost
            if (ip == "::1")
            {
                try
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
                }
                catch (Exception ex)
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                }
            }
            return new string[] { ip.ToString() };
        }



        IEnumerable<string> IEmailHelper.GetClientIP(string localIp)
        {
            throw new NotImplementedException();
        }


    }
}


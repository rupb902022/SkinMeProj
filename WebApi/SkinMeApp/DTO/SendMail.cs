using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace SkinMeApp.DTO
{
    public  class SendMail 
    {
        string Projectmail = "rupb902022@gmail.com";
        string Password = "bgroup90";
        string Host = "smtp.gmail.com";
        int Port = 587;

        

        public void SendingMail(RegisterUser user, EventArgs e )
        {

            SmtpClient client = new SmtpClient(Host, Port)
            {
                Credentials = new NetworkCredential(Projectmail, Password),
                EnableSsl = true,
               
            };

            string subject = "change your password ";

            string body = $"Hello {user.user_firstName}  ";

            client.Send("rupb902022@gmail.com",user.user_email , subject, body);
            
        }


        

    }
}
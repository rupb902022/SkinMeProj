﻿using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;
using DATA;
using SkinMeApp.DTO;

namespace SkinMeApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogInController : ApiController
    {
        bgroup90_test2Entities14 db = new bgroup90_test2Entities14();

        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

        //[HttpGet]
        //[Route("api/mail")]
        //public  IHttpActionResult SendMail()
        //{
        //    string strNewPassword = GeneratePassword().ToString();

        //    string Projectmail = "rupb902022@gmail.com";
        //    string Password = "oqodhdtqfpxmhivc";
        //    string Host = "smtp.gmail.com";
        //    int Port = 587;

        //    try
        //    {
        //        SmtpClient client = new SmtpClient(Host, Port)
        //        {
        //            Credentials = new NetworkCredential(Projectmail, Password),
        //            EnableSsl = true,

        //        };

        //        string subject = "Reset your password ";

        //        string body = $"Hello , \n" + " \n" +
        //                      $"This is your temporary password :" + "  " + strNewPassword + "\n" + "\n" +
        //                      $"In order to reset your password, go back to Skinme login page and enter this temporary password, " +
        //                      $"after that you will be able to change it for your a password of your choice. " + "\n" + "\n" +
        //                      "Best Regards,\n Skinme Team ";


        //        client.Send("rupb902022@gmail.com", "elise.wenger25@gmail.com", subject, body);
        //        return Ok("mail sent ");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }

        //}

        [HttpPut]
        [Route("api/mail/forgotpassword")]
        public IHttpActionResult UpdateTempPassword(string  mail) 
        {

            string strNewPassword = GeneratePassword().ToString();

            string Projectmail = "rupb902022@gmail.com";
            string Password = "oqodhdtqfpxmhivc";
            string Host = "smtp.gmail.com";
            int Port = 587;

            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.email == mail);

                SmtpClient client = new SmtpClient(Host, Port)
                {
                    Credentials = new NetworkCredential(Projectmail, Password),
                    EnableSsl = true,

                };

                string subject = "Reset your password ";

                string body = $"Hello , \n" + " \n" +
                              $"This is your temporary password :" + "  " + strNewPassword + "\n" + "\n" +
                              $"In order to reset your password, go back to Skinme login page and enter this temporary password, " +
                              $"after that you will be able in the settings section to change it for the password of your choice. " + "\n" + "\n" +
                              "Best Regards,\n Skinme Team ";

                if (user != null)
                {


                    user.user_password= strNewPassword;
                    db.SaveChanges();
                    client.Send("rupb902022@gmail.com", mail, subject, body);
                    return Ok("  mail sent   ");

                }
                return Content(HttpStatusCode.NotFound,
                   $" not found.");





            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult Get()
        {
            try

            {
                return Ok(db.AppUsers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Route("api/LogIn/User")]

        public IHttpActionResult LogInUser([FromBody] Logincheck login)
        {
            try
            {
                AppUser log = db.AppUsers.FirstOrDefault
                    (x => x.username == login.username && x.user_password == login.user_password);

                if (log != null)
                {
                    return Content(HttpStatusCode.OK,log);
                }
                return Content(HttpStatusCode.NotFound,
                    $"username or password were not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/LogIn/Cos")]

        public IHttpActionResult LogInCos([FromBody] LogincheckCos loginc)
        {
            try
            {
                AppCosmetologist logc = db.AppCosmetologists.FirstOrDefault
                    (x => x.cosmetologist_user_name == loginc.cosmetologist_user_name && x.cosmetologist_user_password == loginc.cosmetologist_user_password);

                if (logc != null)
                {
                    return Content(HttpStatusCode.OK,
                        $"{ logc.cosmetologist_id}");
                }
                return Content(HttpStatusCode.NotFound,
                    $"username or password were not found");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/LogIn/register")]

        public IHttpActionResult Post([FromBody] AppUser value)

        {
            try
            {
                db.AppUsers.Add(value);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.appUser_id), value);

            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/LogIn/SocialMediaLogin")]
        public IHttpActionResult SocialMediaLogin([FromBody] SocialMediaLogin value)
        {
            try
            {
                AppUser social = new AppUser();
                if (social.appUser_id == 0)
                {
                    social.first_name = value.first_name;
                    social.email = value.user_email;
                    //social.picture = value.user_profilepic;

                    db.AppUsers.Add(social);
                    db.SaveChanges();
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + value.appUser_id), value);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Could not connect to your social media ");

            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    

        //public IHttpActionResult Put(int id, [FromBody] AppUser value)
        //{
        //    try
        //    {
        //        AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
        //        if (user != null)
        //        {
        //            user.user_firstName = value.user_firstName;
        //            user.user_lastName = value.user_lastName;
        //            user.user_email = value.user_email;
        //            user.username = value.username;
        //            user.user_password = value.user_password;
        //            return Ok(user);
        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"User with id={id} was not found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPut]
        [Route("api/UpdateUser")]
        public IHttpActionResult UpdateUserInfo(int id, [FromBody] UpdateUserInfo up) // update user info dto
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    
                    user.email = up.email;
                    user.username = up.username;
                    user.user_password = up.user_password;
                    //user.picture = up.picture;

                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/UpdateCos")]

        public IHttpActionResult UpdateCosInfo(int id, [FromBody] UpdateCosInfo up) // update cosmetologist info dto
        {
            try
            {
                AppCosmetologist user = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == id);
                if (user != null)
                {
                    
                    user.cosmetologist_email = up.email;
                    user.cosmetologist_user_name = up.username;
                    user.cosmetologist_user_password = up.user_password;
                    user.cosmetic_license_num = up.cosmetic_license_num;
                    user.cosmetic_businessName = up.cosmetic_businessName;
                    user.cosmetic_city = up.cosmetic_city;
                    user.cosmetic_address = up.cosmetic_address;
                    user.cosmetologist_phoneNumber = up.cosmetologist_phoneNumber;

                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Cosmetologist with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/login/UpdatePassword")]
        public IHttpActionResult ChangePassword(int id, [FromBody] ForgotPassword forgot) // update user password after temp password 
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.user_password = forgot.user_password;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User with username={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/login/UpdateCosPassword")]
        public IHttpActionResult ChangeCosPassword(int id, [FromBody] ForgotCosPassword forgot) // update cos password after temp password 
        {
            try
            {
                AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == id);
                if (cos != null)
                {
                    cos.cosmetologist_user_password = forgot.cosmetologist_user_password;
                    db.SaveChanges();
                    return Ok(cos);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Cosmetologist with username={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


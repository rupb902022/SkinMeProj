using System;
using System.Collections.Generic;
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
        bgroup90_prodEntities1 db = new bgroup90_prodEntities1();


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
        public IHttpActionResult UpdateTempPassword(string mail)
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
                    user.user_password = strNewPassword;
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
        [HttpPut]
        [Route("api/mail/forgotcospassword")]
        public IHttpActionResult UpdateTempPasswordCos(string mail)
        {
            string strNewPassword = GeneratePassword().ToString();

            string Projectmail = "rupb902022@gmail.com";
            string Password = "oqodhdtqfpxmhivc";
            string Host = "smtp.gmail.com";
            int Port = 587;

            try
            {
                AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_email == mail);

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

                if (cos != null)
                {
                    cos.cosmetologist_user_password = strNewPassword;
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

        public IHttpActionResult LogInUser([FromBody] Logincheck login) // login user
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
        [Route("api/LogIn/User/SocialMedia")]

        public IHttpActionResult SocialMediaLogin([FromBody] Logincheck login)
        {
            try
            {
                AppUser log = db.AppUsers.FirstOrDefault
                    (x => x.username == login.username && x.email == login.email);

                if (log != null)
                {
                    return Content(HttpStatusCode.OK, log);
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

                if (logc != null && logc.cosmetic_status!="Pending")
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
        [Route("api/LogIn/Cos")]

        public IHttpActionResult LogInCos([FromBody] LogincheckCos loginc) // login cos
        {
            try
            {
                AppCosmetologist logc = db.AppCosmetologists.FirstOrDefault
                    (x => x.cosmetologist_user_name == loginc.cosmetologist_user_name && x.cosmetologist_user_password == loginc.cosmetologist_user_password);

                if (logc != null && logc.cosmetic_status != "Pending")
                {
                    return Content(HttpStatusCode.OK,
                        $"{logc.cosmetologist_id}");
                }
                else if (logc != null && logc.cosmetic_status == "Pending")
                {
                    return Content(HttpStatusCode.Conflict,
               $"משתמש טרם אושר");
                }
                else
                {
                    return Content(HttpStatusCode.NotFound,
             $"שם משתמש או סיסמה אינם נכונים");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("api/LogIn/register")]
        public IHttpActionResult Post([FromBody] AppUser value) // regular user register to app
        {
            try
            {
                List<AppCosmetologist> cosmetologists = db.AppCosmetologists.ToList(); // bring all cosmetologists in order to check email 
                List<AppUser> users = db.AppUsers.ToList(); // bring all users in order to check email

                foreach (AppCosmetologist cos in cosmetologists)
                {
                    if (cos.cosmetologist_email == value.email)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Email address={value.email} already exist.");
                    }
                    else if (cos.cosmetologist_user_name == value.username)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Username={value.username} already exist.");
                    }
                }
                foreach (AppUser user in users)
                {
                    if (user.email == value.email)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Email address={value.email} already exist.");
                    }
                    else if (user.username == value.username)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Username={value.username} already exist.");
                    }
                }
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("api/LogIn/registerCos")]
        public IHttpActionResult CosRegister([FromBody] AppCosmetologist value) // cosmetologist register to app

        {
            try
            {
                List<AppCosmetologist> cosmetologists = db.AppCosmetologists.ToList(); // bring all cosmetologists in order to check email 
                List<AppUser> users = db.AppUsers.ToList(); // bring all users in order to check email

                foreach (AppCosmetologist cos in cosmetologists)
                {
                    if (cos.cosmetologist_email == value.cosmetologist_email)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Email address={value.cosmetologist_email} already exist.");
                    }
                    else if (cos.cosmetologist_user_name == value.cosmetologist_user_name)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Username={value.cosmetologist_user_name} already exist.");
                    }
                }
                foreach (AppUser user in users)
                {
                    if (user.email == value.cosmetologist_email)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Email address={value.cosmetologist_email} already exist.");
                    }
                    else if (user.username == value.cosmetologist_user_name)
                    {
                        return Content(HttpStatusCode.Conflict,
                    $"Username={value.cosmetologist_user_name} already exist.");
                    }
                }
                db.AppCosmetologists.Add(value);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.cosmetologist_id), value);

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
            catch (Exception ex)
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
                    $"User with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("api/login/UpdateUserEmail")]
        public IHttpActionResult ChangeUserEmail(int id, [FromBody] UpdateUserInfo info) // update cos email 
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.email = info.email;
                    db.SaveChanges();
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
        [Route("api/login/UpdateUserUsername")]
        public IHttpActionResult ChangeUserUsername(int id, [FromBody] UpdateUserInfo info) // update cos email 
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.username = info.username;
                    db.SaveChanges();
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
        [Route("api/login/UpdateCosPassword")]
        public IHttpActionResult ChangeCosPassword(int id, [FromBody] ForgotCosPassword forgot) // update cos password 
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
                    $"Cosmetologist with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/login/UpdateCosEmail")]
        public IHttpActionResult ChangeCosEmail(int id, [FromBody] ChangeInfoCos info) // update cos email 
        {
            try
            {
                AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == id);
                if (cos != null)
                {
                    cos.cosmetologist_email = info.cosmetologist_email;
                    db.SaveChanges();
                    return Ok(cos);
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
        [Route("api/login/UpdateCosPhone")]
        public IHttpActionResult ChangeCosPhoneNumber(int id, [FromBody] ChangeInfoCos info) // update cos phone number
        {
            try
            {
                AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == id);
                if (cos != null)
                {
                    cos.cosmetologist_phoneNumber = info.cosmetologist_phoneNumber;
                    db.SaveChanges();
                    return Ok(cos);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Cosmetologist with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


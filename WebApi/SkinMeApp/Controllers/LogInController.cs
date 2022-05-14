using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DATA;
using SkinMeApp.DTO;

namespace SkinMeApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LogInController : ApiController
    {
        bgroup90_test2Entities5 db = new bgroup90_test2Entities5();

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
        [Route("api/LogIn")]

        public IHttpActionResult LogIn([FromBody] Logincheck login)
        {
            try
            {
                AppUsers log = db.AppUsers.FirstOrDefault
                    (x => x.username == login.userName && x.user_password == login.password);

                if (log != null)
                {
                    return Content(HttpStatusCode.OK,
                        $"Valid user ");
                }
                return Content(HttpStatusCode.NotFound,
                    $"username or password were not found");

            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("api/LogIn/register")]

        public IHttpActionResult Post([FromBody] AppUsers value)

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
        }

        [HttpPost]
        [Route("api/LogIn/SocialMediaLogin")]
        public IHttpActionResult SocialMediaLogin([FromBody] SocialMediaLogin value)
        {
            try
            {
                AppUsers social = new AppUsers();
                if (social.appUser_id == 0)
                {
                    social.first_name = value.first_name;
                    social.email = value.user_email;
                    social.picture = value.user_profilepic;

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
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    
                    user.email = up.email;
                    user.username = up.username;
                    user.user_password = up.user_password;
                    user.picture = up.picture;

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
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    
                    user.email = up.email;
                    user.username = up.username;
                    user.user_password = up.user_password;
                    user.cosmetic_license_num = up.cosmetic_license_num;
                    user.cosmetic_businessName = up.cosmetic_businessName;
                    user.cosmetic_city = up.cosmetic_city;
                    user.cosmetic_address = up.cosmetic_address;

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
        [Route("api/login/ForgotPassword")]
        public IHttpActionResult ForgotPassword(string username, [FromBody] ForgotPassword forgot) // update user info dto
        {
            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.username == username);
                if (user != null)
                {

                    user.user_password = forgot.user_password;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User with username={username} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


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
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class LogInController : ApiController
    {
        bgroup90_test2Entities2 db = new bgroup90_test2Entities2();

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

        public IHttpActionResult LogIn([FromBody] Logincheck login )
        {
            try
            {
                AppUser log = db.AppUsers.FirstOrDefault
                    (x => x.username ==login.userName && x.user_password ==login.password);

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
        }

        public IHttpActionResult Put(int id, [FromBody] AppUser value)
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.user_firstName = value.user_firstName;
                    user.user_lastName = value.user_lastName;
                    user.user_email = value.user_email;
                    user.username = value.username;
                    user.user_password = value.user_password;
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


    }
    }


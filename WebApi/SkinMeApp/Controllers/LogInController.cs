using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DATA;

namespace SkinMeApp.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class LogInController : ApiController
    {
        PjDbContext db = new PjDbContext();

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
        public IHttpActionResult Get(string userName, string password)
        {
            try
            {
                AppUser log = db.AppUsers.FirstOrDefault
                    (x => x.username == userName && x.user_password == password);

                if (log != null)
                {
                    return Content(HttpStatusCode.OK,
                        $"user id: {log.appUser_id } user role:{log.user_role}");
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
          
            
        }
    }


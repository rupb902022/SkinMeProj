using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA;

namespace SkinMeApp.Controllers
{
    public class CosmetologistsController : ApiController
    {
        bgroup90_test2Entities1 db = new bgroup90_test2Entities1();

        // GET api/<controller>
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

        // GET api/<controller>/5
        public IHttpActionResult Get(string cosmetic_licence_num)
        {
            try
            {
                AppUser log = db.AppUsers.user_role.contains(''cosmetic'')
                    

                if (log != null)
                {
                    return Content(HttpStatusCode.OK,
                        $"Cosmetic licence number: {log.cosmetic_licence_num}Cosmetologit name: {log.cosmetic_businessName} Cosmetologit Adress:{log.osmetic_address},{log.cosmetic_city} Cosmetologit speciality: {log.cosmetic_speciality}");
                  
                }
                return Content(HttpStatusCode.NotFound,
                    $"username or password were not found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
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
    public class CosmetologistsController : ApiController
    {
        PjDbContext db = new PjDbContext();

        //GET api/<controller>
        public IHttpActionResult Get()
        {
            
            try
            {
                
                return Ok(db.AppUsers.Select(x => x.user_firstName).ToList());

            try
            {
                if (db.AppUsers(x => x.user_role == 'cosmetic')
                {
                    return $"userName: {x.userName}";
                }
                else
                {
                    return "error";
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string userrole = "cosmetic")
        {
            try
            {
                List<AppUser> users = db.AppUsers.Where(x => x.user_role == userrole).ToList();

                if (users != null)
                {
                    foreach (AppUser u in users)
                    {
                        Console.WriteLine(u.user_firstName);
                    }
                    return Content(HttpStatusCode.OK, users);


                }



                return Content(HttpStatusCode.NotFound,
                    $"no cosmetologist found");
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public IHttpActionResult Get(string userName)
        //{
        //    try
        //    {
        //        AppUser log = db.AppUsers.user_role.contains('cosmetic');


        //        if (log != null)
        //        {
        //            return Content(HttpStatusCode.OK,
        //                $"Cosmetic licence number: {log.cosmetic_licence_num}Cosmetologit name: {log.cosmetic_businessName} Cosmetologit Adress:{log.osmetic_address},{log.cosmetic_city} Cosmetologit speciality: {log.cosmetic_speciality}");

        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"username or password were not found");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

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
using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SkinMeApp.Controllers
{
    public class CosController : ApiController
    {
        bgroup90DbContext db = new bgroup90DbContext();
        public IHttpActionResult Get(string userrole = "cosmetic") // get only cosmetologist
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
        public IHttpActionResult Get() // Get all care plans 
        {
            try
            {
                return Ok(db.CarePlans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


    }
}

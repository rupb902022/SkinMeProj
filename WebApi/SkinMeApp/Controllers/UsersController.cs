using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SkinMeApp.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        
        bgroup90DbContext db = new bgroup90DbContext();

        public IHttpActionResult Get(string userrole = "user") // get only cosmetologist
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
                    $"no user found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

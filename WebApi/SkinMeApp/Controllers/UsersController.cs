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

        bgroup90_SkinmeDbContext db = new bgroup90_SkinmeDbContext();

        public IHttpActionResult Get(string userrole = "User") // get only users
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

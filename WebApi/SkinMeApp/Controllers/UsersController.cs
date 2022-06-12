using DATA;
using SkinMeApp.DTO;
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

        bgroup90_test2Entities8 db = new bgroup90_test2Entities8();

        [HttpPut]
        [Route("api/Users/addroute")]
        public IHttpActionResult Update(int id,[FromBody] UserMaslul maslul) // can update only manual / instructions
        {
            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id );
                if (user!= null)
                {
                    user.user_route = maslul.user_route;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Users/addmaslul")]
        public IHttpActionResult AddMaslul(int id, [FromBody] UserMaslul maslul) // update user after purchase of maslul
        {
            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.user_route = maslul.user_route;
                    user.user_period = maslul.user_period;
                    user.user_dermatology = maslul.user_dermatology;
                    user.user_currentProducts = maslul.user_currentProducts;
                    user.user_sensitive = maslul.user_sensitive;
                    user.user_areas = maslul.user_areas;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Users/addmycos")]
        public IHttpActionResult MyCos(int id, [FromBody] Mycos cos) // Update my cos 
        {
            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.cosmetologist_id = cos.cosmetologist_id;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //    public IHttpActionResult Get(string userrole = "user") // get only cosmetologist
        //    {
        //        try
        //        {
        //            List<AppUsers> users = db.AppUsers.Where(x => x.user_role == userrole).ToList();

        //            if (users != null)
        //            {
        //                foreach (AppUsers u in users)
        //                {
        //                    Console.WriteLine(u.first_name);
        //                }
        //                return Content(HttpStatusCode.OK, users);


        //            }
        //            return Content(HttpStatusCode.NotFound,
        //                $"no user found");
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //}
    }
}

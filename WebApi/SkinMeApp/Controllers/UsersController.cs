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

        bgroup90_prodEntities1 db = new bgroup90_prodEntities1();


        [HttpGet]
        [Route("api/Users/{id}")]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
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


        [HttpGet]
        [Route("api/Users/GetAllAppUsers")]
        public IHttpActionResult allUsers() // Get all users 
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


        [HttpPut]
        [Route("api/Users/profileImage/{id}")]
        public IHttpActionResult Update(int id, [FromBody] AppUser appUser) 
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    user.picture = appUser.picture;
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


        [HttpPost]
        [Route("api/Users/images")] // upload images מעקב תמונות 

        public IHttpActionResult Post([FromBody] UsersImage img)
        {
            try
            {
                db.UsersImages.Add(img);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + img.img_id), img);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Users/allimages/{id}")]
        public IHttpActionResult GetUserImages(int id)
        {
            try
            {
                List<UsersImage> images = db.UsersImages.Where(x => x.appUser_id == id).ToList<UsersImage>();
                if (images != null)
                {
                    return Content(HttpStatusCode.OK, images);
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
        [Route("api/Users/addroute")]
        public IHttpActionResult Update(int id,[FromBody] UserMaslul maslul) // can update only manual / instructions
        {
            try
            {
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id );
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
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
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
                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
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

        [HttpGet]
        [Route("api/User/Mycos")]
        public IHttpActionResult GetMyCos(int id ) 
        {
            try
            {

                AppUser user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);

                if (user != null)
                {
                    AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == user.cosmetologist_id);
                    if (cos!=null)
                    {
                        return Content(HttpStatusCode.OK,cos);
                    }

                }
                return Content(HttpStatusCode.NotFound,
                    $"no cos  found");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

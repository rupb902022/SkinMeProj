using DATA;
using SkinMeApp.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        bgroup90_DbSkinme db = new bgroup90_DbSkinme();
        private object openFileDialoge1;

        [HttpGet]
        [Route("api/getskintype/")]
        public IHttpActionResult Get(int id) // get skintype
        {
            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
               
                return Ok(user.user_skinType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        [HttpPost]
        [Route("api/User/ImageProfile")]

        public IHttpActionResult Post([FromBody] ProfileImage value)

        {
            try
            {
                db.ProfileImage.Add(value);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.imgId), value);

            }
            
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("api/getImage/")]
        public IHttpActionResult GetImage(int id) // get skintype
        {
            try
            {
                ProfileImage user = db.ProfileImage.SingleOrDefault(x => x.imgId == id);

                return Ok(user.img);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        [Route("api/Users/profileImage")]
        public IHttpActionResult UploadProfilePic(int id, FileUpload fileobj) // upload to existant user profile image
        {

            try
            {
                AppUsers user = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (user != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        fileobj.file.CopyTo(ms);
                        var filebytes = ms.ToArray();
                        user.picture = filebytes;

                        db.SaveChanges();
                        return Ok(user);
                    }
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
                      return Content(HttpStatusCode.OK,
                          
                          $"id: {user.cosmetologist_id }");

                    


                }
                return Content(HttpStatusCode.NotFound,
                    $"no products for plan found");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

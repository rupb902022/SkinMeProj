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
    public class CosController : ApiController
    {
        bgroup90_eliseofek db = new bgroup90_eliseofek();
        //public IHttpActionResult Get(string userrole = "Cosmetologist") // get only cosmetologist
        //{
        //    try
        //    {
        //        List<AppUsers> users = db.AppUsers.Where(x => x.user_role == userrole).ToList();

        //        if (users != null)
        //        {
        //            foreach (AppUsers u in users)
        //            {
        //                Console.WriteLine(u.first_name + u.cosmetic_address + u.cosmetic_city);
        //            }
        //            return Content(HttpStatusCode.OK, users);


        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"no cosmetologist found");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public IHttpActionResult Get() // Get all תוכניות טיפוח
        {
            try
            {
                return Ok(db.SkinPlan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] SkinPlan value) // add plan
        {
            try
            {
                db.SkinPlan.Add(value);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.plan_id), value);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IHttpActionResult Put(int id, [FromBody] SkinPlan value) // Update plan 
        //{
        //    try
        //    {
        //        SkinPlan s = db.SkinPlans.SingleOrDefault(x => x.plan_id == id);
        //        if (s != null)
        //        {
        //            s.plan_name = value.plan_name;
        //            s.plan_date = value.plan_date;
        //            s.notes = value.notes;
        //            s.Product = value.Product;  /// ? how to change products 

        //            return Ok(s);
        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"Plan with id={id} was not found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        public IHttpActionResult Put(int id, [FromBody] PlanUpdate value) // Update plan 
        {
            try
            {
                SkinPlan s = db.SkinPlan.SingleOrDefault(x => x.plan_id == id);
                if (s != null)
                {
                    s.plan_name = value.plan_name;
                    s.plan_date = value.plan_date;
                    s.notes = value.notes;
                    List<Products> products = db.Products.ToList(); /// ? how to change products from the plan

                    return Ok(s);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Plan with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult Delete(int id) // Delete plan 
        {
            try
            {
                SkinPlan s = db.SkinPlan.SingleOrDefault(x => x.plan_id == id);
                if (s != null)
                {
                    db.SkinPlan.Remove(s);
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"Plan with id={id} was not found to delete");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }



        //public IHttpActionResult Get() // Get all תוכניות טיפוח
        //{
        //    try
        //    {
        //        return Ok(db.SkinPlans);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);

        //    }
        //}

        [HttpGet]
        [Route("api/Cos/GetDepending")]
        public IHttpActionResult GetDepending(string status = "waiting") // get users that are waiting for cosmetologist
        {
            try
            {
                List<AppUsers> users = db.AppUsers.Where(x => x.user_status == status && x.user_route != "1").ToList();

                if (users != null)
                {
                    foreach (AppUsers u in users)
                    {   
                        Console.WriteLine(u.appUser_id + u.first_name + u.user_route);
                    }
                    return Content(HttpStatusCode.OK, users);


                }
                return Content(HttpStatusCode.NotFound,
                    $"no waiting users found");
            }
            catch (Exception)
            {

                throw;
            }
        }
        //[HttpGet]
        //[Route("api/Cos/GetClients")]
        //public IHttpActionResult GetClients(string role = "User") // get clients for cosmetologist
        //{
        //    try
        //    {
        //        List<AppUser> users = db.AppUsers.Where(x => x.user_role == role && x.skin != "1").ToList();

        //        if (users != null)
        //        {
        //            foreach (AppUser u in users)
        //            {
        //                Console.WriteLine(u.appUser_id + u.first_name + u.user_route);
        //            }
        //            return Content(HttpStatusCode.OK, users);


        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"no waiting users found");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}

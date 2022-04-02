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
        SkinmeDbContext db = new SkinmeDbContext();
        public IHttpActionResult Get(string userrole = "Cosmetologist") // get only cosmetologist
        {
            try
            {
                List<AppUser> users = db.AppUsers.Where(x => x.user_role == userrole).ToList();

                if (users != null)
                {
                    foreach (AppUser u in users)
                    {
                        Console.WriteLine(u.user_firstName + u.cosmetic_address + u.cosmetic_city);
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
        public IHttpActionResult Get() // Get all תוכניות טיפוח
        {
            try
            {
                return Ok(db.SkinPlans);
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
                db.SkinPlans.Add(value);
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
                SkinPlan s = db.SkinPlans.SingleOrDefault(x => x.plan_id == id);
                if (s != null)
                {
                    s.plan_name = value.plan_name;
                    s.plan_date = value.plan_date;
                    s.notes = value.notes;
                    List<Product> products = db.Products.ToList(); /// ? how to change products from the plan

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
                SkinPlan s = db.SkinPlans.SingleOrDefault(x => x.plan_id == id);
                if (s != null)
                {
                    db.SkinPlans.Remove(s);
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

        public IHttpActionResult Put(int id, [FromBody] MapAddress value) // Add Address for cosmetic business
        {
            try
            {
                AppUser s = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);
                if (s != null)
                {
                    s.cosmetic_address = value.cosmetic_address;
                    s.cosmetic_city = value.cosmetic_city;

                    return Ok(s);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Cosmetic with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}

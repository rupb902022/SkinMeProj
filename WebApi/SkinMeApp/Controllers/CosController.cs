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
        bgroup90_SkinmeDbContext db = new bgroup90_SkinmeDbContext();

        [HttpGet]
        [Route("api/map")]
        public IHttpActionResult allcos() // Get all estheticians 
        {
            try
            {
                return Ok(db.AppCosmetologists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

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



        [HttpPost]
        [Route("api/Cos/Depending")]
        public IHttpActionResult GetDepending([FromBody] Depending status) // get users that are waiting for cosmetologist
        {
            try
            {
                List<AppUsers> users = db.AppUsers.Where(x => x.user_status == status.user_status).ToList();

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("api/Cos/GetClients")]
        public IHttpActionResult GetClients([FromBody] Clients id) // get clients for cosmetologist
        {
            try
            {
                List<AppUsers> users = db.AppUsers.Where(x => x.cosmetologist_id == id.cosmetologist_id).ToList();

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



        [HttpPut]
        [Route("api/Cos/SearchFamiliar")]
        public IHttpActionResult SearchFamiliar([FromBody] User id) // search familiar users like this 
        {
            int smart_count = 0; // this is the smart count that will show us how high is the similarity between 2 users in every check

            try
            {
                List<AppUsers> users = db.AppUsers.ToList();

                if (users != null)
                {
                    foreach (AppUsers u in users) // check every characteristic of my person comparing to other users (each user at a time)
                    {
                        string temp_new_profile_name = ""; // will save temp name for new profile. if needed, we will use it at the end of the checks

                        if (u.user_skinType != null && u.user_skinType == id.user_skinType) 
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_skinType;
                        }
                        else if (u.user_skinProblem != null && u.user_skinProblem == id.user_skinProblem)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_skinProblem;

                        }
                        else if (u.user_cheek != null && u.user_cheek == id.user_cheek)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_cheek;

                        }
                        else if (u.user_Tzone != null && u.user_Tzone == id.user_Tzone)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_Tzone;

                        }
                        else if (u.user_sunExposure != null && u.user_sunExposure == id.user_sunExposure)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_sunExposure;

                        }
                        else if (u.user_stress != null && u.user_stress == id.user_stress)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_stress;

                        }
                        else if (u.user_period != null && u.user_period == id.user_period)
                        {
                            smart_count++;
                            temp_new_profile_name += id.user_period;

                        }


                        if (smart_count >= 6 && u.profile_code != null) // if the comparing is at high score, so put the same profile code to the new user
                        {
                            id.profile_code = u.profile_code.Value;

                            // bring the specific line of this profile (by profile code) and save the profile name
                            Profiles profile = db.Profiles.SingleOrDefault(x => x.profile_code == id.profile_code);
                            // add +1 to users_count
                            profile.users_count++;
                        }
                        else if (smart_count >= 6 && u.profile_code == null) // if the comparing is at high score but there is not profile that exist to these characastics, create new profile
                        {
                            //Here we add new profile with the name: temp_new_profile_name


                            //Profiles profile = db.Profiles.Add() -----not working
                            //InsertNewProfile(temp_new_profile_name); ------- not working
                        }
                    }
                }
                return Content(HttpStatusCode.OK, users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        //public string InsertNewProfile(string profileName)
        //{
        //    Profiles profile = db.Profiles.Add(Profiles.profile_name=profileName);

        //    db.SaveChanges();
        //    return Created(new Uri(Request.RequestUri.AbsoluteUri + value.appUser_id), value);

        //}

    }

}




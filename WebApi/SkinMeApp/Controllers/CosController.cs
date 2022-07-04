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
        bgroup90_Db db = new bgroup90_Db();
        

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
                return Ok(db.SkinPlans);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        [Route("api/Cos/AddSkinPlan")]
        public IHttpActionResult AddPlan([FromBody] SkinPlan value) // add plan
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

        [HttpPost]
        [Route("api/Cos/AddProdToPlan")]
        public IHttpActionResult AddPTP(int id, [FromBody] ProdForPlan value) // add products to skin plan
        {
            try
            {
                SkinPlan s = db.SkinPlans.SingleOrDefault(x => x.plan_id == id);
                if (s != null)
                {
                    Products_for_plan p = new Products_for_plan();
                    p.prod_id = value.prod_id;
                    p.plan_id = id;
                    db.Products_for_plan.Add(p);
                    db.SaveChanges();
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

        [HttpPost]
        [Route("api/Cos/Depending")]
        public IHttpActionResult GetDepending([FromBody] Depending status) // get users that are waiting for cosmetologist
        {
            try
            {
                List<AppUser> users = db.AppUsers.Where(x => x.user_status == status.user_status).ToList();

                if (users != null)
                {
                    foreach (AppUser u in users)
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
                List<AppUser> users = db.AppUsers.Where(x => x.cosmetologist_id == id.cosmetologist_id).ToList();

                if (users != null)
                {
                    foreach (AppUser u in users)
                    {
                        Console.WriteLine(u.appUser_id + u.first_name + u.user_route + u.user_gender + u.user_skinProblem + u.user_skinType);
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



        [HttpPost]
        [Route("api/Cos/SearchFamiliar")]
        public IHttpActionResult SearchFamiliar(int id) // search familiar users like this 
        {
            AppUser newUser = db.AppUsers.SingleOrDefault(x => x.appUser_id == id);


            try
            {
                List<AppUser> users = db.AppUsers.ToList();

                if (users != null)
                {
                    foreach (AppUser u in users) // check every characteristic of my person comparing to other users (each user at a time)
                    {
                        string temp_new_profile_name = ""; // will save temp name for new profile. if needed, we will use it at the end of the checks
                        int smart_count = 0; // this is the smart count that will show us how high is the similarity between 2 users in every check


                        if (u.user_skinType != null && u.user_skinType == newUser.user_skinType)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_skinType;
                        }
                        if (u.user_skinProblem != null && u.user_skinProblem == newUser.user_skinProblem)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_skinProblem;

                        }
                        if (u.user_cheek != null && u.user_cheek == newUser.user_cheek)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_cheek;

                        }
                        if (u.user_Tzone != null && u.user_Tzone == newUser.user_Tzone)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_Tzone;

                        }
                        if (u.user_sunExposure != null && u.user_sunExposure == newUser.user_sunExposure)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_sunExposure;

                        }
                        if (u.user_stress != null && u.user_stress == newUser.user_stress)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_stress;

                        }
                        if (u.user_period != null && u.user_period == newUser.user_period)
                        {
                            smart_count++;
                            temp_new_profile_name += newUser.user_period;

                        }

                        if (smart_count >= 7 && u.profile_code == null) // if the comparing is at high score, but there is no profile code to both users: create new profile and give it to both of them
                        {
                            Profile p = new Profile();
                            p.profile_name = temp_new_profile_name;
                            db.Profiles.Add(p);
                            db.SaveChanges();

                            //add the profile_code to the u and for the new_user
                            newUser.profile_code = p.profile_code;
                            u.profile_code = p.profile_code;
                            db.SaveChanges();

                            // find the u skin plan id 
                            //bring the products for plan of this plan
                            List<Products_for_plan> productsForPlan = db.Products_for_plan.Where(x => x.plan_id == u.plan_id).ToList();

                            // put these products in our profile 
                            foreach (Products_for_plan pfp in productsForPlan)
                            {
                                ProductsForProfile forProfile = new ProductsForProfile();
                                forProfile.prod_id = pfp.prod_id;
                                db.ProductsForProfiles.Add(forProfile);
                                db.SaveChanges();
                            }
                        }
                        else if (smart_count >= 7 && u.profile_code != null) // if the comparing is at high score, but the comperd user has code- so put the same profile code to the new user
                        {
                            newUser.profile_code = u.profile_code.Value;

                            //AppUsers newUser = db.AppUsers.SingleOrDefault(x => x.appUser_id == id.appUser_id);
                            db.SaveChanges();

                            // find the u skin plan id 
                            //bring the products for plan of this plan
                            List<Products_for_plan> productsForPlan = db.Products_for_plan.Where(x => x.plan_id == u.plan_id).ToList();

                            // put these products in our profile 
                            foreach (Products_for_plan pfp in productsForPlan)
                            {
                                ProductsForProfile forProfile = new ProductsForProfile();
                                forProfile.profile_code = u.profile_code;
                                forProfile.prod_id = pfp.prod_id;
                                db.ProductsForProfiles.Add(forProfile);
                                db.SaveChanges();
                            }
                            return Ok(newUser);
                        }
                    }
                }
                return Content(HttpStatusCode.OK, "No matching profile was found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("api/Cos/RateCosmetologist")]
        public IHttpActionResult RateCosmetologist(int rate, [FromBody] Cosmetologist id) // Rate cosmetologist
        {
            try
            {
                AppCosmetologist cos = db.AppCosmetologists.SingleOrDefault(x => x.cosmetologist_id == id.cosmetologist_id);
                if (cos != null)
                {
                    cos.cosmetologist_sumRate += rate; 
                    cos.cosmetologist_numOfRates++; 
                    cos.cosmetologist_rate = cos.cosmetologist_sumRate / cos.cosmetologist_numOfRates++; 
                    db.SaveChanges();

                    return Ok(cos);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Cosmetologist with id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}




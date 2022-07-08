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

    public class ProductsController : ApiController
    {
        bgroup90_prodEntities db = new bgroup90_prodEntities();

        [HttpGet]
        [Route("api/Products/")]
        public IHttpActionResult Get() // get all products
        {
            try
            {
                return Ok(db.Products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        [Route("api/Products/status")]
        public IHttpActionResult ApprovedProducts(string status = "Approved") // get only approved products 
        {
            try
            {
                List<Product> prod = db.Products.Where(x => x.prod_status == status).ToList();

                if (prod != null)
                {
                    foreach (Product p in prod)
                    {
                        Console.WriteLine(p.prod_id);
                    }
                    return Content(HttpStatusCode.OK, prod);


                }
                return Content(HttpStatusCode.NotFound,
                    $"no approved products found");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/Products/oilyskin")]

        public IHttpActionResult OilySkin(string skintype = "oily") // get only products for oily skin 
        {

            try
            {
                List<Product> prod = db.Products.Where(x => x.prod_type == "oily day").Take(3).ToList();

                if (prod != null)
                {
                    foreach (Product p in prod)
                    {
                        Console.WriteLine(p.prod_id);
                    }
                    return Content(HttpStatusCode.OK, prod);


                }

                return Content(HttpStatusCode.NotFound,
                                   $"no product found");

            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPost]
        [Route("api/Products/byskintypeday")]
        public IHttpActionResult ProductsbySkinTypeDay([FromBody] GetSkintype skintype)
        {

            if (skintype.user_skinType == "שומני")
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "oily day").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (skintype.user_skinType == "מעורב")
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "regular d").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");


                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "dry d").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");


                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

        [HttpPost]
        [Route("api/Products/byskintypenight")]
        public IHttpActionResult ProductsbySkinTypeNight([FromBody] GetSkintype skintype)
        {

            if (skintype.user_skinType == "שומני")
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "oily n").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (skintype.user_skinType == "מעורב")
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "regular n").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");


                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {
                    List<Product> prod = db.Products.Where(x => x.prod_type == "dry n").ToList();

                    if (prod != null)
                    {
                        foreach (Product p in prod)
                        {
                            Console.WriteLine(p.prod_id);
                            break;
                        }
                        return Content(HttpStatusCode.OK, prod);


                    }
                    return Content(HttpStatusCode.NotFound,
                                       $"no product found");


                }
                catch (Exception)
                {

                    throw;
                }
            }


        }




        [HttpPost]
        [Route("api/Products/addprod")]

        public IHttpActionResult Post([FromBody] Product value)
        {
            try
            {
                db.Products.Add(value);
                db.SaveChanges();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.prod_name), value);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //public IHttpActionResult Put(int id, [FromBody] Product value) 
        //{
        //    try
        //    {
        //        Product p = db.Products.SingleOrDefault(x => x.prod_id == id);
        //        if (p != null)
        //        {
        //            p.prod_description = value.prod_description;
        //            p.prod_manual = value.prod_manual;
        //            return Ok(p);
        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"Product with id={id} was not found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPut]
        [Route("api/Products/update")]
        public IHttpActionResult Update(string name, string company, [FromBody] UpdateProduct prod) // can update only manual / instructions
        {
            try
            {
                Product p = db.Products.SingleOrDefault(x => x.prod_name == name && x.prod_company == company);
                if (p != null)
                {
                    p.prod_manual = prod.prod_manual;
                    db.SaveChanges();
                    return Ok(p);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Product with those details was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Product prod = db.Products.SingleOrDefault(x => x.prod_id == id);
                if (prod != null)
                {
                    db.Products.Remove(prod);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"Product with id={id} was not found to delete");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Products/GetProdForPlan")]
        public IHttpActionResult GetProductsForPlan(int id) // get products for skin plan
        {
            
            try
            {
                SkinPlan plan = db.SkinPlans.SingleOrDefault(x => x.appUser_id == id); // get the plan for this user

                List<ProductsForPlan> productsForPlan = db.ProductsForPlans.Where(x => x.plan_id == plan.plan_id).ToList(); //list all the products for this plan id

                List<Product> prod = db.Products.ToList();

                if (productsForPlan != null)
                {
                    foreach (ProductsForPlan p in productsForPlan)
                    {
                        return Ok(p);
                    }
                }
                return Content(HttpStatusCode.NotFound,
                    $"Products for plan id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet]
        //[Route("api/Products/GetProdForAutoPlan")]
        //public IHttpActionResult GetProductsForAutoPlan(int id) // get products for skin plan
        //{
        //    try
        //    {
        //        AppUser plan = db.AppUsers.SingleOrDefault(x => x.plan_id == id); // get the plan for this user

        //        List<ProductsForPlan> productsForPlan = db.ProductsForPlans.Where(x => x.plan_id == plan.plan_id).ToList(); //list all the products for this plan id

        //        if (productsForPlan != null)
        //        {
        //            foreach (ProductsForPlan p in productsForPlan)
        //            {
        //                foreach(Product products in prod)
        //                {
        //                    if (p.prod_id==products.prod_id)
        //                    {
        //                        return Ok(products);
        //                    }

                            
        //                }

        //                //return Ok(p);
        //            }
        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"Products for plan id={id} was not found.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet]
        [Route("api/Products/GetProdForAutoPlanDay")]
        public IHttpActionResult GetProductsForAutoPlanDay(int id) // get products for plan
        {
            try
            {
                AppUser plan = db.AppUsers.SingleOrDefault(x => x.appUser_id == id); // get the plan for this user

                List<ProductsForPlan> productsForPlan = db.ProductsForPlans.Where(x => x.plan_id == plan.plan_id).ToList(); //list all the products for this plan id

                List<Product> prod = db.Products.ToList();
                List<Product> finalp = new List<Product>();

                    if (productsForPlan != null)
                {
                    foreach (ProductsForPlan p in productsForPlan)
                    {
                        foreach (Product products in prod)
                        {
                            if (p.prod_id == products.prod_id && products.prod_time =="D" && !finalp.Contains(products))
                            {
                                    finalp.Add(products);
                            }
                        }
                    }
                    return Ok(finalp);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Products for plan id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Products/GetProdForAutoPlanNight")]
        public IHttpActionResult GetProductsForAutoPlanNight(int id) // get products for plan
        {
            try
            {
                AppUser plan = db.AppUsers.SingleOrDefault(x => x.appUser_id == id); // get the plan for this user

                List<ProductsForPlan> productsForPlan = db.ProductsForPlans.Where(x => x.plan_id == plan.plan_id).ToList(); //list all the products for this plan id

                List<Product> prod = db.Products.ToList();
                List<Product> finalp = new List<Product>();

                if (productsForPlan != null)
                {
                    foreach (ProductsForPlan p in productsForPlan)
                    {
                        foreach (Product products in prod)
                        {
                            if (p.prod_id == products.prod_id && products.prod_time == "N" && !finalp.Contains(products))
                            {

                                finalp.Add(products);


                            }

                        }

                    }
                    return Ok(finalp);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Products for plan id={id} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Products/showproddetails")]
        public IHttpActionResult ProductDetails([FromBody] ProductInfo prod)
        {

            try
            {
                Product log = db.Products.FirstOrDefault
                    (x => x.prod_id == prod.prod_id);

                if (log != null)
                {
                    return Content(HttpStatusCode.OK,
                        $"Valid product, ID:{log.prod_id} desc: { log.prod_description}  recommended: {log.prod_manual}");
                }

                return Content(HttpStatusCode.NotFound,
                                   $"no product found");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("api/Products/RateProd/{id}")]
        public IHttpActionResult RateProduct(int id, [FromBody] RateProd rating) // Rate cosmetologist
        {
            try
            {
                Product prod = db.Products.SingleOrDefault(x => x.prod_id == id);



                if (prod != null)
                {
                    prod.prod_sumRate += rating.prod_sumRate;
                    prod.prod_numOfRates++;
                    double res = (double)prod.prod_sumRate / (double)prod.prod_numOfRates;
                    prod.prod_rate = Convert.ToDouble(String.Format("{0:0.00}", res));
                    db.SaveChanges();
                    return Ok(prod);
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

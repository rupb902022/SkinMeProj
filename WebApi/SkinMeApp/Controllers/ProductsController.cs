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
        bgroup90_DbSkinme db = new bgroup90_DbSkinme();

       



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

            if (skintype.user_skinType == "oily")
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
            else if (skintype.user_skinType == "regular")
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

            if (skintype.user_skinType == "oily")
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
            else if (skintype.user_skinType == "regular")
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
        public IHttpActionResult GetProductsForPlan(int plan_id) // get products for skin plan
        {
            try
            {

                List<Products_for_plan> productsForPlan = db.Products_for_plan.Where(x => x.plan_id == plan_id).ToList();

                if (productsForPlan != null)
                {
                    foreach (Products_for_plan p in productsForPlan)
                    {
                        return Content(HttpStatusCode.OK, $" {p.prod_id }");

                    }


                }
                return Content(HttpStatusCode.NotFound,
                    $"no products for plan found");
            }
            catch (Exception)
            {
                throw;
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
    }
}

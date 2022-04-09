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
        bgroup90_test2Entities3 db = new bgroup90_test2Entities3();

        public IHttpActionResult Get()
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
        [HttpPost]
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
        [Route("api/Products")]
        public IHttpActionResult Update(int id, [FromBody] AddProduct prod ) // can uptade only manual / instructions
        {
            try
            {
                Product p = db.Products.SingleOrDefault(x => x.prod_id == id);
                if (p != null)
                {
                    p.prod_manual = prod.prod_manual;
                   
                    return Ok(p);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Product with id={id} was not found.");
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
    }
}

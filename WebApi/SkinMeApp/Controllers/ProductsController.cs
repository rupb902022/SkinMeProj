using DATA;
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
        bgroup90_test2Entities2 db = new bgroup90_test2Entities2();

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
        public IHttpActionResult Put(string name, [FromBody] Product value) // change to id , add prod id in DB
        {
            try
            {
                Product p = db.Products.SingleOrDefault(x => x.prod_name == name);
                if (p != null)
                {
                    p.prod_description = value.prod_description;
                    p.prod_manual = value.prod_manual;
                    return Ok(p);
                }
                return Content(HttpStatusCode.NotFound,
                    $"Product with name={name} was not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IHttpActionResult Delete(string name) // change to id when add id in product db 
        {
            try
            {
                Product prod = db.Products.SingleOrDefault(x => x.prod_name == name);
                if (prod != null)
                {
                    db.Products.Remove(prod);
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"Product with name={name} was not found to delete");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

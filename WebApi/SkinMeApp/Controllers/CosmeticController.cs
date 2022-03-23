using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA;

namespace SkinMeApp.Controllers
{
    public class CosmeticController : ApiController
    {
        // GET api/<controller>
        public List<string> Get()
        {
            bgroup90_test2Entities1 db = new bgroup90_test2Entities1();

            return db.AppUsers.Where(x => x.user_role == x.user_role.ToString()).Select (x=> x.username).ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
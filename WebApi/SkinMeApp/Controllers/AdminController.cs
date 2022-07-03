
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
    public class AdminController : ApiController
    {
        bgroup90_test2Entities15 db = new bgroup90_test2Entities15();

        //public IHttpActionResult Post ([FromBody] Admin admin)
        //{
        //    try
        //    {
        //        ApprovedCo cos = db.ApprovedCos.FirstOrDefault
        //            (x => x.cosmetic_license_num ==admin.cosmetic_license_num );

        //        if (cos != null)
        //        {
        //            return Content(HttpStatusCode.OK,
        //                $"Valid license number ");
        //        }
        //        return Content(HttpStatusCode.NotFound,
        //            $"license number was not found");

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}

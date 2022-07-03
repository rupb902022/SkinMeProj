using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class PlanUpdate
    {
        public int plan_id;
        public string plan_name { get; set; }
        public DateTime plan_date { get; set; }
        public string notes { get; set; }

        public List<Product> Products;
      

    }
}
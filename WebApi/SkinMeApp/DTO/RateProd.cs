using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class RateProd
    {
        public int prod_id { get; set; }

        public double prod_rate { get; set; }
        public int prod_sumRate { get; set; }
        public int prod_numOfRates { get; set; }

    }
}
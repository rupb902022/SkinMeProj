using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class CosmetologistRate
    {
        public int cosmetologist_id { get; set; }
        public int cosmetologist_sumRate { get; set; }
        public int cosmetologist_numOfRates { get; set; }
        public int cosmetologist_rate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class Clients
    {
        //cosmetologist
        public int cosmetologist_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

        //user
        public int appUser_id { get; set; }
        public string full_name { get; set; }
        public string user_status { get; set; }
        public string user_route { get; set; }
    }
}
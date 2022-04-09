using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class GetClients
    {
        public int appUser_id { get; set; }
        public int cosmetic_license_num { get; set; }
        public string username { get; set; }
        public string cosmetic_businessName { get; set; }
        public string user_role { get; set; }


    }
}
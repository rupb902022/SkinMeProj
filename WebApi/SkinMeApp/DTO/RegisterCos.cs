using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class RegisterCos
    {
        public int appUser_id { get; set; }
        public string username { get; set; }
        public string user_password { get; set; }
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_email { get; set; }
        public byte[] user_profilepic { get; set; }
        public int cosmetic_license_num { get; set; }
        public string cosmetic_businessName { get; set; }
        public string cosmetic_address { get; set; }
        public string cosmetic_city { get; set; }
        public string cosmetic_speciality { get; set; }
    }
}
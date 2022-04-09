using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class SocialMediaLogin
    {
        public int appUser_id { get; set; }
        public string full_name { get; set; }
        public string user_email { get; set; }

        public byte[] user_profilepic { get; set; }

        public string user_skinType { get; set; }
        public string user_skinProblem { get; set; }
        public string user_cheek { get; set; }
        public string user_Tzone { get; set; }
        public string user_sunExposure { get; set; }
        public string user_stress { get; set; }

    }
}
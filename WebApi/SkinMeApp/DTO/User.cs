using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class User
    {
        public int appUser_id { get; set; }
        public string user_skinType { get; set; }
        public string user_skinProblem { get; set; }
        public string user_cheek { get; set; }
        public string user_Tzone { get; set; }
        public string user_sunExposure { get; set; }
        public string user_stress { get; set; }
        public string user_period { get; set; }
        public int profile_code { get; set; }

    }
}
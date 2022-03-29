using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class RegisterUser
    {
        public int appUser_id { get; set; }
        public string username { get; set; }
        public string user_password { get; set; }
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_email { get; set; }
        public string user_gender { get; set; }
        public Nullable<System.DateTime> user_birth { get; set; }
        public byte[] user_profilepic { get; set; }
        public string user_role { get; set; }
        public string user_skinType { get; set; }
        public string user_skinProblem { get; set; }
        public string user_cheek { get; set; }
        public string user_Tzone { get; set; }
        public string user_sunExposure { get; set; }
        public string user_stress { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class ForgotCosPassword
    {
        public string cosmetologist_user_name { get; set; }
        public string cosmetologist_user_password { get; set; }

        public string cosmetologist_email { get; set; }
    }
}
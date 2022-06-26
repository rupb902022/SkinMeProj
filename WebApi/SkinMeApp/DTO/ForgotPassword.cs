using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class ForgotPassword
    {
        public string username { get; set; }
        public string user_password { get; set; }

        public string email { get; set; }

    }
}